using CarInfoApp.Models;
using CarInfoApp.Models.ViewModels;
using CarInfoApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarInfoApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INhtsaService _nhtsaService;
    public HomeController(ILogger<HomeController> logger, INhtsaService nhtsaService)
    {
        _logger = logger;
        _nhtsaService = nhtsaService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new CarSearchViewModel();

        try
        {

            var makesResponse = await _nhtsaService.GetAllMakes();
            if (makesResponse.IsSuccess && makesResponse.Data != null)
            {
                viewModel.Makes = makesResponse.Data;
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to load car makes. Please try again.";
            }
            var currentYear = DateTime.Now.Year;
            viewModel.AvailableYears = Enumerable.Range(1995, currentYear - 1995 + 2).Reverse().ToList();
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while loading the page. Please try again.";
        }

        return View(viewModel);
    }



    [HttpPost]
    public async Task<IActionResult> Search(CarSearchViewModel model)
    {
        try
        {

            var makesResponse = await _nhtsaService.GetAllMakes();
            if (makesResponse.IsSuccess && makesResponse.Data != null)
            {
                model.Makes = makesResponse.Data;
            }

            var currentYear = DateTime.Now.Year;
            model.AvailableYears = Enumerable.Range(1995, currentYear - 1995 + 2).Reverse().ToList();


            if (model.SelectedMakeId <= 0)
            {
                ViewBag.ErrorMessage = "Please select a car make.";
                return View("Index", model);
            }

            if (model.SelectedYear < 1995 || model.SelectedYear > currentYear + 1)
            {
                ViewBag.ErrorMessage = "Please select a valid year.";
                return View("Index", model);
            }


            var vehicleTypesResponse = await _nhtsaService.GetVehicleTypesForMake(model.SelectedMakeId);
            if (vehicleTypesResponse.IsSuccess && vehicleTypesResponse.Data != null)
            {
                model.VehicleTypes = vehicleTypesResponse.Data;
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to load vehicle types for the selected make.";
                return View("Index", model);
            }


            var modelsResponse = await _nhtsaService.GetModelsForMakeAndYear(model.SelectedMakeId, model.SelectedYear);
            if (modelsResponse.IsSuccess && modelsResponse.Data != null)
            {
                model.Models = modelsResponse.Data;
                model.HasSearched = true;
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to load models for the selected criteria.";
                return View("Index", model);
            }

            if (model.Models.Count == 0)
            {
                ViewBag.InfoMessage = "No models found for the selected criteria.";
            }
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while searching. Please try again.";
        }

        return View("Index", model);
    }




    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
