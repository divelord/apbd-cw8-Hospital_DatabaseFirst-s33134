using apbd_cw8_Hospital_DatabaseFirst_s33134.DTOs;
using apbd_cw8_Hospital_DatabaseFirst_s33134.Exceptions;
using apbd_cw8_Hospital_DatabaseFirst_s33134.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw8_Hospital_DatabaseFirst_s33134.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    // GET /api/patients
    // GET /api/patients?search={an}
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? search)
    {
        try
        {
            var result = await _dbService.GetAllPatientsAsync(search);

            return Ok(result);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    // POST /api/patients/{pesel}/bedassignments
    [Route("{pesel}/bedassignments")]
    [HttpPost]
    public async Task<IActionResult> Post(string pesel, [FromBody] CreatePatientBedAssignmentDto dto)
    {
        try
        {
            await _dbService.CreatePatientBedAssignmentAsync(pesel, dto);

            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
}