namespace CarInfoApp.Models.ViewModels;

public class CarSearchViewModel
{

    public CarSearchViewModel()
    {
        Makes = new List<CarMake>();
        VehicleTypes = new List<VehicleType>();
        Models = new List<CarModel>();
        AvailableYears = new List<int>();
    }
    public int SelectedMakeId { get; set; }
    public int SelectedYear { get; set; }
    public int SelectedVehicleTypeId { get; set; }

    public List<CarMake> Makes { get; set; }
    public List<VehicleType> VehicleTypes { get; set; }
    public List<CarModel> Models { get; set; }

    public List<int> AvailableYears { get; set; }

    public bool HasSearched { get; set; } = false;
}
