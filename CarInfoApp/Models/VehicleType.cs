namespace CarInfoApp.Models;

public class VehicleType
{
    public int VehicleTypeId { get; set; }
    public string VehicleTypeName { get; set; }
}
public class VehicleTypeResponse
{
    public VehicleTypeResponse()
    {
        Results = new List<VehicleType>();
    }
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public List<VehicleType> Results { get; set; }
}