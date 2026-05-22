namespace apbd_cw8_Hospital_DatabaseFirst_s33134.DTOs;

public class CreatePatientBedAssignmentDto
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public string BedType { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
}