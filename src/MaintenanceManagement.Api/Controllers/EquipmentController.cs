using MaintenanceManagement.Api.Data;
using MaintenanceManagement.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaintenanceManagement.Api.Models;

namespace MaintenanceManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly MaintenanceDbContext _db;

    public EquipmentController(MaintenanceDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentListItemDto>>> GetEquipment()
    {
        var equipment = await _db.Equipment
            .AsNoTracking()
            .Join(
                _db.Sites,
                e => e.SiteId,
                s => s.SiteId,
                (e, s) => new { e, s })
            .Join(
                _db.Companies,
                x => x.s.CompanyId,
                c => c.CompanyId,
                (x, c) => new { x.e, x.s, c })
            .Join(
                _db.Countries,
                x => x.s.CountryId,
                co => co.CountryId,
                (x, co) => new EquipmentListItemDto
                {   EquipmentId = x.e.EquipmentId,
                    EquipmentCode = x.e.EquipmentCode,
                    EquipmentName = x.e.EquipmentName,
                    EquipmentStatus = x.e.EquipmentStatus,
                    SiteCode = x.s.SiteCode,
                    SiteName = x.s.SiteName,
                    CompanyCode = x.c.CompanyCode,
                    CompanyName = x.c.CompanyName,
                    CountryCode = co.CountryCode,
                    CountryName = co.CountryName
                })
            .ToListAsync();

        return Ok(equipment);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EquipmentListItemDto>> GetEquipmentById(int id)
    {
        var equipment = await _db.Equipment
            .AsNoTracking()
            .Where(e => e.EquipmentId == id)
            .Join(
                _db.Sites,
                e => e.SiteId,
                s => s.SiteId,
                (e, s) => new { e, s })
            .Join(
                _db.Companies,
                x => x.s.CompanyId,
                c => c.CompanyId,
                (x, c) => new { x.e, x.s, c })
            .Join(
                _db.Countries,
                x => x.s.CountryId,
                co => co.CountryId,
                (x, co) => new EquipmentListItemDto
                {   EquipmentId = x.e.EquipmentId,
                    EquipmentCode = x.e.EquipmentCode,
                    EquipmentName = x.e.EquipmentName,
                    EquipmentStatus = x.e.EquipmentStatus,
                    SiteCode = x.s.SiteCode,
                    SiteName = x.s.SiteName,
                    CompanyCode = x.c.CompanyCode,
                    CompanyName = x.c.CompanyName,
                    CountryCode = co.CountryCode,
                    CountryName = co.CountryName
                })
            .FirstOrDefaultAsync();

        if (equipment is null)
        {
            return NotFound();
        }

        return Ok(equipment);
    }
    [HttpPost]
    public async Task<ActionResult<EquipmentListItemDto>> CreateEquipment(CreateEquipmentDto dto)
    {
        var equipment = new Equipment
        {
            SiteId = dto.SiteId,
            EquipmentCode = dto.EquipmentCode,
            EquipmentName = dto.EquipmentName,
            Description = dto.Description,
            SerialNumber = dto.SerialNumber,
            Manufacturer = dto.Manufacturer,
            ModelNumber = dto.ModelNumber,
            EquipmentStatus = dto.EquipmentStatus,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Equipment.Add(equipment);

        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetEquipmentById),
            new { id = equipment.EquipmentId },
            new EquipmentListItemDto
            {   EquipmentId = equipment.EquipmentId,
                EquipmentCode = equipment.EquipmentCode,
                EquipmentName = equipment.EquipmentName,
                EquipmentStatus = equipment.EquipmentStatus
            });
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EquipmentListItemDto>> UpdateEquipment(
        int id,
        UpdateEquipmentDto dto)
    {
        var equipment = await _db.Equipment
            .FirstOrDefaultAsync(e => e.EquipmentId == id);

        if (equipment is null)
        {
            return NotFound();
        }

        equipment.SiteId = dto.SiteId;
        equipment.EquipmentCode = dto.EquipmentCode;
        equipment.EquipmentName = dto.EquipmentName;
        equipment.Description = dto.Description;
        equipment.SerialNumber = dto.SerialNumber;
        equipment.Manufacturer = dto.Manufacturer;
        equipment.ModelNumber = dto.ModelNumber;
        equipment.EquipmentStatus = dto.EquipmentStatus;
        equipment.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok(new EquipmentListItemDto
        {  EquipmentId = equipment.EquipmentId,
            EquipmentCode = equipment.EquipmentCode,
            EquipmentName = equipment.EquipmentName,
            EquipmentStatus = equipment.EquipmentStatus
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        var equipment = await _db.Equipment
            .FirstOrDefaultAsync(e => e.EquipmentId == id);
    
        if (equipment is null)
        {
            return NotFound();
        }
    
        _db.Equipment.Remove(equipment);
    
        await _db.SaveChangesAsync();
    
        return NoContent();
    }
}