namespace Ambev.DeveloperEvaluation.Application._Shared;

public class AddressResult
{
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public GeoLocationResult Geo { get; set; } = new(); 
    public class GeoLocationResult
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
