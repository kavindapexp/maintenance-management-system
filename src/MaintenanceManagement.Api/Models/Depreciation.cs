namespace MaintenanceManagement.Api.Models;

public class Depreciation
{
    public int DepreciationId { get; set; }

    public int EquipmentId { get; set; }

    public DateTime DepreciationDate { get; set; }

    public decimal DepreciationAmount { get; set; }

    public decimal AccumulatedAmount { get; set; }

    public int CurrencyId { get; set; }

    public string Method { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}