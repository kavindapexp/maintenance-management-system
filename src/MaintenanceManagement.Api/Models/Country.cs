namespace MaintenanceManagement.Api.Models;

public class Country
{
    public int CountryId { get; set; }

    public string CountryCode { get; set; } = string.Empty;

    public string CountryName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}