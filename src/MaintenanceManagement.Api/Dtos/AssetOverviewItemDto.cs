namespace MaintenanceManagement.Api.Dtos;

public class AssetOverviewItemDto
{
    public int EquipmentId { get; set; }

    public string AssetCode { get; set; } = string.Empty;

    public string AssetName { get; set; } = string.Empty;

    public string Owner { get; set; } = string.Empty;

    public decimal AssetValue { get; set; }

    public decimal Depreciation { get; set; }

    public decimal FinalValue { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;
}