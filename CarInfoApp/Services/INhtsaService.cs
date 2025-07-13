using CarInfoApp.Models;

namespace CarInfoApp.Services;

public interface INhtsaService
{
    Task<ApiResponse<List<CarMake>>> GetAllMakes();
    Task<ApiResponse<List<VehicleType>>> GetVehicleTypesForMake(int makeId);
    Task<ApiResponse<List<CarModel>>> GetModelsForMakeAndYear(int makeId, int year);
}
