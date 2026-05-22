namespace apbd_cw8_Hospital_DatabaseFirst_s33134.DTOs;

public class GetPatientDetailsDto
{
    public string Pesel { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Sex { get; set; } = string.Empty;
    public List<GetPatientAdmissionDetailsDto> Admissions { get; set; } = [];
    public List<GetPatientBedAssignmentDetailsDto> BedAssignments { get; set; } = [];
}

public class GetPatientAdmissionDetailsDto
{
    public int Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public GetPatientAdmissionWardDetailsDto Ward { get; set; } = null!;
}

public class GetPatientAdmissionWardDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public String Description { get; set; } = string.Empty;
}

public class GetPatientBedAssignmentDetailsDto
{
    public int Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public GetPatientBedAssignmentBedDetailsDto Bed { get; set; } = null!;
}

public class GetPatientBedAssignmentBedDetailsDto
{
    public int Id { get; set; }
    public GetPatientBedAssignmentBedTypeDetailsDto BedType { get; set; } = null!;
    public GetPatientBedAssignmentBedRoomDetailsDto Room { get; set; } = null!;
}

public class GetPatientBedAssignmentBedTypeDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class GetPatientBedAssignmentBedRoomDetailsDto
{
    public string Id { get; set; } = string.Empty;
    public bool HasTv { get; set; }
    public GetPatientBedAssignmentBedRoomWardDetailsDto Ward { get; set; } = null!;
}

public class GetPatientBedAssignmentBedRoomWardDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}