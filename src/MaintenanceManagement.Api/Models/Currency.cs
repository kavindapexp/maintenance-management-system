namespace MaintenanceManagement.Api.Models;

public class Currency
{
    public int CurrencyId { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public string CurrencyName { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public short DecimalPlaces { get; set; } = 2;

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; } = true;
}