using apbd_cw8_Hospital_DatabaseFirst_s33134.DTOs;

namespace apbd_cw8_Hospital_DatabaseFirst_s33134.Services;

public interface IDbService
{
    Task<IEnumerable<GetPatientDetailsDto>> GetAllPatientsAsync(string? search);
    Task CreatePatientBedAssignmentAsync(string pesel, CreatePatientBedAssignmentDto dto);
}