using ApartmentRental.Core.DTO;
using ApartmentRental.Core.Services;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRental.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApartmentRentalController : ControllerBase
{
    private readonly IApartmentService _apartmentService;

    public ApartmentRentalController(IApartmentService apartmentService)
    {
        _apartmentService = apartmentService;
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateNewApartment([FromBody] ApartmentCreationRequestDto dto)
    {
        try
        {
            await _apartmentService.AddNewApartmentToExistingLandLordAsync(dto);
        }
        catch (EntityNotFoundException)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpGet(template: "GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _apartmentService.GetAllApartmentsBasicInfosAsync());
    }

    [HttpGet(template: "GetTheCheapest")]
    public async Task<IActionResult> GetTheCheapestApartment()
    {
        var apartment = await _apartmentService.GetTheCheapestApartmentAsync();
        return Ok(apartment);
    }
    
}