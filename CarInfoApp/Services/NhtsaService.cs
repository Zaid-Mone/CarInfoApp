using CarInfoApp.Models;
using Newtonsoft.Json;

namespace CarInfoApp.Services;

public class NhtsaService : INhtsaService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public NhtsaService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["NhtsaApi:BaseUrl"] ?? "https://vpic.nhtsa.dot.gov/api";

    }

    public async Task<ApiResponse<List<CarMake>>> GetAllMakes()
    {
        try
        {

            var url = $"{_baseUrl}/vehicles/getallmakes?format=json";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<CarMakeResponse>(content);

                if (apiResponse?.Results != null)
                {
                    var sortedMakes = apiResponse.Results
                        .OrderBy(m => m.Make_Name)
                        .ToList();


                    return new ApiResponse<List<CarMake>>
                    {
                        IsSuccess = true,
                        Data = sortedMakes
                    };
                }
            }

            return new ApiResponse<List<CarMake>>
            {
                IsSuccess = false,
                ErrorMessage = $"API request failed with status: {response.StatusCode}"
            };
        }

        catch (Exception ex)
        {
            return new ApiResponse<List<CarMake>>
            {
                IsSuccess = false,
                ErrorMessage = $"Sommething went wrong : {ex.Message}"
            };
        }
    }

    public async Task<ApiResponse<List<VehicleType>>> GetVehicleTypesForMake(int makeId)
    {
        try
        {

            var url = $"{_baseUrl}/vehicles/GetVehicleTypesForMakeId/{makeId}?format=json";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<VehicleTypeResponse>(content);

                if (apiResponse?.Results != null)
                {
                    var sortedTypes = apiResponse.Results
                        .OrderBy(vt => vt.VehicleTypeName)
                        .ToList();


                    return new ApiResponse<List<VehicleType>>
                    {
                        IsSuccess = true,
                        Data = sortedTypes
                    };
                }
            }

            return new ApiResponse<List<VehicleType>>
            {
                IsSuccess = false,
                ErrorMessage = $"API request failed with status: {response.StatusCode}"
            };
        }

        catch (Exception ex)
        {
            return new ApiResponse<List<VehicleType>>
            {
                IsSuccess = false,
                ErrorMessage = $"Sommething went wrong : {ex.Message}"
            };
        }
    }

    public async Task<ApiResponse<List<CarModel>>> GetModelsForMakeAndYear(int makeId, int year)
    {
        try
        {

            var url = $"{_baseUrl}/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<CarModelResponse>(content);

                if (apiResponse?.Results != null)
                {
                    var sortedModels = apiResponse.Results
                        .GroupBy(m => m.Model_Name)
                        .Select(g => g.First())
                        .OrderBy(m => m.Model_Name)
                        .ToList();


                    return new ApiResponse<List<CarModel>>
                    {
                        IsSuccess = true,
                        Data = sortedModels
                    };
                }
            }

            return new ApiResponse<List<CarModel>>
            {
                IsSuccess = false,
                ErrorMessage = $"API request failed with status: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CarModel>>
            {
                IsSuccess = false,
                ErrorMessage = $"Sommething went wrong : {ex.Message}"
            };
        }
    }

}