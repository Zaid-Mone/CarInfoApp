using CarInfoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarInfoApp.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly INhtsaService _nhtsaService;

    public ApiController(INhtsaService nhtsaService)
    {
        _nhtsaService = nhtsaService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllMakes()
    {
        try
        {
            var response = await _nhtsaService.GetAllMakes();
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(new { error = response.ErrorMessage });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicleTypes(int makeId)
    {
        try
        {
            if (makeId <= 0)
            {
                return BadRequest(new { error = "Invalid make ID" });
            }

            var response = await _nhtsaService.GetVehicleTypesForMake(makeId);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(new { error = response.ErrorMessage });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetModels(int makeId, [FromQuery] int year)
    {
        try
        {
            if (makeId <= 0)
            {
                return BadRequest(new { error = "Invalid make ID" });
            }

            if (year < 1900 || year > DateTime.Now.Year + 1)
            {
                return BadRequest(new { error = "Invalid year" });
            }

            var response = await _nhtsaService.GetModelsForMakeAndYear(makeId, year);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(new { error = response.ErrorMessage });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }



}
