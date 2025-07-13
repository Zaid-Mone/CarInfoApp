namespace CarInfoApp.Models;

public class CarMake
{
    public int Make_ID { get; set; }
    public string Make_Name { get; set; }
}
public class CarMakeResponse
{

    public CarMakeResponse()
    {
        Results = new List<CarMake>();
    }
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public List<CarMake> Results { get; set; }
}