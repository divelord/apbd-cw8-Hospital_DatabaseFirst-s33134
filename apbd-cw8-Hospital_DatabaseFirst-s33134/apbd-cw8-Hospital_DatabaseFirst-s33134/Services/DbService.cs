using apbd_cw8_Hospital_DatabaseFirst_s33134.Data;
using apbd_cw8_Hospital_DatabaseFirst_s33134.DTOs;
using apbd_cw8_Hospital_DatabaseFirst_s33134.Exceptions;
using apbd_cw8_Hospital_DatabaseFirst_s33134.Models;
using Microsoft.EntityFrameworkCore;

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
        var patients = await _dbContext.Patients
            .Where(x => string.IsNullOrEmpty(search) || x.FirstName.Contains(search) || x.LastName.Contains(search))
            .Select(x => new GetPatientDetailsDto
            {
                Pesel = x.Pesel,
                Firstname = x.FirstName,
                Lastname = x.LastName,
                Age = x.Age,
                Sex = x.Sex ? "Male" : "Female",
                Admissions = x.Admissions.Select(xa => new GetPatientAdmissionDetailsDto
                {
                    Id = xa.Id,
                    AdmissionDate = xa.AdmissionDate,
                    DischargeDate = xa.DischargeDate,
                    Ward = new GetPatientAdmissionWardDetailsDto
                    {
                        Id = xa.Ward.Id,
                        Name = xa.Ward.Name,
                        Description = xa.Ward.Description
                    }
                }).ToList(),
                BedAssignments = x.BedAssignments.Select(xb => new GetPatientBedAssignmentDetailsDto
                {
                    Id = xb.Id,
                    From = xb.From,
                    To = xb.To,
                    Bed = new GetPatientBedAssignmentBedDetailsDto
                    {
                        Id = xb.Bed.Id,
                        BedType = new GetPatientBedAssignmentBedTypeDetailsDto
                        {
                            Id = xb.Bed.BedType.Id,
                            Name = xb.Bed.BedType.Name,
                            Description = xb.Bed.BedType.Description
                        },
                        Room = new GetPatientBedAssignmentBedRoomDetailsDto
                        {
                            Id = xb.Bed.Room.Id,
                            HasTv = xb.Bed.Room.HasTv,
                            Ward = new GetPatientBedAssignmentBedRoomWardDetailsDto
                            {
                                Id = xb.Bed.Room.Ward.Id,
                                Name = xb.Bed.Room.Ward.Name,
                                Description = xb.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            }).ToListAsync();

        if (!patients.Any())
        {
            throw new NotFoundException($"Brak pacjentów dla wyszukiwania: {search}");
        }

        return patients;
    }

    public async Task CreatePatientBedAssignmentAsync(string pesel, CreatePatientBedAssignmentDto dto)
    {
        var anyPatient = await _dbContext.Patients.AnyAsync(x => x.Pesel == pesel);

        if (!anyPatient)
        {
            throw new NotFoundException("Brak pacjenta o podanym ID");
        }

        if (dto.To.HasValue && dto.From > dto.To.Value)
        {
            throw new BadRequestException("Data From musi być wcześniej od daty To");
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var availableBed = await _dbContext.Beds
                .FirstOrDefaultAsync(x => x.BedType.Name == dto.BedType
                                          && x.Room.Ward.Name == dto.Ward
                                          && !x.BedAssignments.Any(xx =>
                                              xx.From < (dto.To ?? DateTime.MaxValue)
                                              && (xx.To == null || xx.To > dto.From)
                                          ));

            if (availableBed == null)
            {
                throw new NotFoundException("Nie ma wolnego łóżka o danym typie w danym oddziale w podanym terminie");
            }

            var bedAssignment = new BedAssignment
            {
                PatientPesel = pesel,
                BedId = availableBed.Id,
                From = dto.From,
                To = dto.To
            };

            await _dbContext.BedAssignments.AddAsync(bedAssignment);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}