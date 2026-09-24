namespace MaintenanceManagement.Api.Dtos;

public class CreateEquipmentDto
{
    public int SiteId { get; set; }

    public string EquipmentCode { get; set; } = string.Empty;

    public string EquipmentName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? SerialNumber { get; set; }

    public string? Manufacturer { get; set; }

    public string? ModelNumber { get; set; }

    public string EquipmentStatus { get; set; } = string.Empty;
}