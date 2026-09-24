using MaintenanceManagement.Api.Data;
using MaintenanceManagement.Api.Dtos;
using MaintenanceManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceManagement.Api.Controllers;

[ApiController]
[Route("api/business")]
public class BusinessController : ControllerBase
{
    private readonly MaintenanceDbContext _db;

    public BusinessController(MaintenanceDbContext db)
    {
        _db = db;
    }

    [HttpGet("countries")]
    public Task<List<Country>> Countries() => _db.Countries.AsNoTracking().OrderBy(x => x.CountryName).ToListAsync();

    [HttpGet("companies")]
    public Task<List<Company>> Companies() => _db.Companies.AsNoTracking().OrderBy(x => x.CompanyName).ToListAsync();

    [HttpGet("sites")]
    public Task<List<Site>> Sites() => _db.Sites.AsNoTracking().OrderBy(x => x.SiteName).ToListAsync();

    [HttpGet("equipment")]
    public Task<List<Equipment>> Equipment() => _db.Equipment.AsNoTracking().OrderBy(x => x.EquipmentName).ToListAsync();

    [HttpGet("maintenance-requests")]
    public Task<List<MaintenanceRequest>> MaintenanceRequests() => _db.MaintenanceRequests.AsNoTracking().OrderByDescending(x => x.RequestedAt).ToListAsync();

    [HttpGet("asset-values")]
    public Task<List<AssetValue>> AssetValues() => _db.AssetValues.AsNoTracking().OrderByDescending(x => x.AcquisitionDate).ToListAsync();

    [HttpGet("depreciations")]
    public Task<List<Depreciation>> Depreciations() => _db.Depreciations.AsNoTracking().OrderByDescending(x => x.DepreciationDate).ToListAsync();

    [HttpGet("asset-valuations")]
    public Task<List<AssetValuation>> AssetValuations() => _db.AssetValuations.AsNoTracking().OrderByDescending(x => x.ValuationDate).ToListAsync();

    [HttpGet("asset-overview")]
    public async Task<ActionResult<List<AssetOverviewItemDto>>> AssetOverview()
    {
        var assets = await (
            from assetValue in _db.AssetValues.AsNoTracking()
            join equipment in _db.Equipment.AsNoTracking()
                on assetValue.EquipmentId equals equipment.EquipmentId
            join site in _db.Sites.AsNoTracking()
                on equipment.SiteId equals site.SiteId
            join company in _db.Companies.AsNoTracking()
                on site.CompanyId equals company.CompanyId
            join currency in _db.Currencies.AsNoTracking()
                on assetValue.CurrencyId equals currency.CurrencyId
            select new
            {
                equipment.EquipmentId,
                AssetCode = equipment.EquipmentCode,
                AssetName = equipment.EquipmentName,
                Owner = company.CompanyName,
                AssetValue = assetValue.AcquisitionCost,
                currency.CurrencyCode
            }).ToListAsync();

        var equipmentIds = assets.Select(x => x.EquipmentId).ToList();
        var depreciationByEquipment = await _db.Depreciations
            .AsNoTracking()
            .Where(x => equipmentIds.Contains(x.EquipmentId))
            .GroupBy(x => x.EquipmentId)
            .Select(group => group
                .OrderByDescending(x => x.DepreciationDate)
                .ThenByDescending(x => x.DepreciationId)
                .Select(x => new { x.EquipmentId, x.AccumulatedAmount })
                .First())
            .ToDictionaryAsync(x => x.EquipmentId, x => x.AccumulatedAmount);

        return Ok(assets.Select(asset =>
        {
            depreciationByEquipment.TryGetValue(asset.EquipmentId, out var depreciation);
            return new AssetOverviewItemDto
            {
                EquipmentId = asset.EquipmentId,
                AssetCode = asset.AssetCode,
                AssetName = asset.AssetName,
                Owner = asset.Owner,
                AssetValue = asset.AssetValue,
                Depreciation = depreciation,
                FinalValue = asset.AssetValue - depreciation,
                CurrencyCode = asset.CurrencyCode
            };
        }).ToList());
    }

    [HttpGet("currencies")]
    public Task<List<Currency>> Currencies() => _db.Currencies.AsNoTracking().OrderBy(x => x.CurrencyCode).ToListAsync();

    [HttpGet("inflation-indexes")]
    public Task<List<InflationIndex>> InflationIndexes() => _db.InflationIndexes.AsNoTracking().OrderByDescending(x => x.PeriodStart).ToListAsync();

    [HttpPost("countries")]
    public Task<ActionResult<Country>> CreateCountry(Country item) => Create(_db.Countries, item);

    [HttpPost("companies")]
    public Task<ActionResult<Company>> CreateCompany(Company item)
    {
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt = item.CreatedAt;
        return Create(_db.Companies, item);
    }

    [HttpPost("sites")]
    public Task<ActionResult<Site>> CreateSite(Site item)
    {
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt = item.CreatedAt;
        return Create(_db.Sites, item);
    }

    [HttpPut("countries/{id:int}")]
    public async Task<ActionResult<Country>> UpdateCountry(int id, Country item)
    {
        var existing = await _db.Countries.FindAsync(id);
        if (existing is null) return NotFound();

        _db.Entry(existing).CurrentValues.SetValues(item);
        existing.CountryId = id;
        await _db.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpPut("companies/{id:int}")]
    public async Task<ActionResult<Company>> UpdateCompany(int id, Company item)
    {
        var existing = await _db.Companies.FindAsync(id);
        if (existing is null) return NotFound();

        _db.Entry(existing).CurrentValues.SetValues(item);
        existing.CompanyId = id;
        existing.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpPut("sites/{id:int}")]
    public async Task<ActionResult<Site>> UpdateSite(int id, Site item)
    {
        var existing = await _db.Sites.FindAsync(id);
        if (existing is null) return NotFound();

        _db.Entry(existing).CurrentValues.SetValues(item);
        existing.SiteId = id;
        existing.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("countries/{id:int}")]
    public Task<IActionResult> DeleteCountry(int id) => Delete<Country>(id);

    [HttpDelete("companies/{id:int}")]
    public Task<IActionResult> DeleteCompany(int id) => Delete<Company>(id);

    [HttpDelete("sites/{id:int}")]
    public Task<IActionResult> DeleteSite(int id) => Delete<Site>(id);

    [HttpPost("maintenance-requests")]
    public Task<ActionResult<MaintenanceRequest>> CreateMaintenanceRequest(MaintenanceRequest item)
    {
        item.RequestedAt = item.RequestedAt == default ? DateTime.UtcNow : item.RequestedAt;
        item.CreatedAt = DateTime.UtcNow;
        item.UpdatedAt = item.CreatedAt;
        return Create(_db.MaintenanceRequests, item);
    }

    [HttpPost("asset-values")]
    public Task<ActionResult<AssetValue>> CreateAssetValue(AssetValue item) => Create(_db.AssetValues, item);

    [HttpPost("depreciations")]
    public Task<ActionResult<Depreciation>> CreateDepreciation(Depreciation item) => Create(_db.Depreciations, item);

    [HttpPost("asset-valuations")]
    public Task<ActionResult<AssetValuation>> CreateAssetValuation(AssetValuation item) => Create(_db.AssetValuations, item);

    [HttpPost("currencies")]
    public Task<ActionResult<Currency>> CreateCurrency(Currency item) => Create(_db.Currencies, item);

    [HttpPost("inflation-indexes")]
    public Task<ActionResult<InflationIndex>> CreateInflationIndex(InflationIndex item) => Create(_db.InflationIndexes, item);

    [HttpPut("maintenance-requests/{id:int}")]
    public Task<ActionResult<MaintenanceRequest>> UpdateMaintenanceRequest(int id, MaintenanceRequest item) => Update(id, item);

    [HttpDelete("maintenance-requests/{id:int}")]
    public Task<IActionResult> DeleteMaintenanceRequest(int id) => Delete<MaintenanceRequest>(id);

    private async Task<ActionResult<T>> Create<T>(DbSet<T> set, T item) where T : class
    {
        set.Add(item);
        await _db.SaveChangesAsync();
        return Ok(item);
    }

    private async Task<ActionResult<MaintenanceRequest>> Update(int id, MaintenanceRequest item)
    {
        var existing = await _db.MaintenanceRequests.FindAsync(id);
        if (existing is null) return NotFound();

        _db.Entry(existing).CurrentValues.SetValues(item);
        existing.MaintenanceRequestId = id;
        existing.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(existing);
    }

    private async Task<IActionResult> Delete<T>(int id) where T : class
    {
        var item = await _db.Set<T>().FindAsync(id);
        if (item is null) return NotFound();

        _db.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}