namespace MaintenanceManagement.Api.Models;

public class AssetValue
{
    public int AssetValueId { get; set; }

    public int EquipmentId { get; set; }

    public decimal AcquisitionCost { get; set; }

    public int CurrencyId { get; set; }

    public DateTime AcquisitionDate { get; set; }

    public DateTime CreatedAt { get; set; }
}