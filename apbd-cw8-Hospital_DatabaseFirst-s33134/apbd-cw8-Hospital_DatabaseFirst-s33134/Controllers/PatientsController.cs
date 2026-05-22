using apbd_cw8_Hospital_DatabaseFirst_s33134.DTOs;
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
        throw new  NotImplementedException();
    }
    
    // POST /api/patients/{pesel}/bedassignments
    [Route("{pesel}/bedassignments")]
    [HttpPost]
    public async Task<IActionResult> Post(string pesel, [FromBody] CreatePatientBedAssignmentDto dto)
    {
        throw new  NotImplementedException();
    }
}