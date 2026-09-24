namespace MaintenanceManagement.Api.Models;

public class Site
{
    public int SiteId { get; set; }

    public int CompanyId { get; set; }

    public string SiteCode { get; set; } = string.Empty;

    public string SiteName { get; set; } = string.Empty;

    public int CountryId { get; set; }

    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = string.Empty;

    public string? PostalCode { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}