using Microsoft.AspNetCore.Mvc;
using Pharmacy.DTOs;
using Pharmacy.Exceptions;
using Pharmacy.Services;

namespace Pharmacy.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PrescriptionController(IDbService dbService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] NewPrescriptionDTO dto)
    {
        try
        {
            await dbService.AddPrescriptionAsync(dto);
            return Created("", null);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}