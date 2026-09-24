namespace MaintenanceManagement.Api.Models;

public class MaintenanceRequest
{
    public int MaintenanceRequestId { get; set; }

    public int EquipmentId { get; set; }

    public string RequestNumber { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}