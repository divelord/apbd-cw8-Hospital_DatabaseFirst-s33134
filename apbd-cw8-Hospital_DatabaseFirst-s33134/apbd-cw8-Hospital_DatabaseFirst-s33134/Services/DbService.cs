using apbd_cw8_Hospital_DatabaseFirst_s33134.Data;
using apbd_cw8_Hospital_DatabaseFirst_s33134.DTOs;

namespace apbd_cw8_Hospital_DatabaseFirst_s33134.Services;

public class DbService : IDbService
{
    private readonly DbfirstContext _dbContext;
    
    public DbService(DbfirstContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetPatientDetailsDto>> GetAllPatientsAsync(string? search)
    {
        throw new NotImplementedException();
    }

    public async Task CreatePatientBedAssignmentAsync(string pesel, CreatePatientBedAssignmentDto dto)
    {
        throw new NotImplementedException();
    }
}