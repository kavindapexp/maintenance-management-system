namespace MaintenanceManagement.Api.Models;

public class AssetValuation
{
    public int AssetValuationId { get; set; }

    public int EquipmentId { get; set; }

    public DateTime ValuationDate { get; set; }

    public decimal ValuationAmount { get; set; }

    public int CurrencyId { get; set; }

    public string ValuationType { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
}