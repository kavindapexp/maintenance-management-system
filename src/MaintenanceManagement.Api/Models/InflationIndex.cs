namespace MaintenanceManagement.Api.Models;

public class InflationIndex
{
    public int InflationIndexId { get; set; }

    public int CountryId { get; set; }

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public decimal IndexValue { get; set; }

    public string? Source { get; set; }

    public DateTime CreatedAt { get; set; }
}