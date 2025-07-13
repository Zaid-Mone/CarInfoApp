namespace CarInfoApp.Models;

public class CarModel
{
    public int Make_ID { get; set; }
    public string Make_Name { get; set; }
    public int Model_ID { get; set; }
    public string Model_Name { get; set; }
}

public class CarModelResponse
{

    public CarModelResponse()
    {
        Results = new List<CarModel>();
    }
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public List<CarModel> Results { get; set; }
}