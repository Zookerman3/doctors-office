namespace DoctorsOffice.Models
{
  public class Patient
  {
    public int PatientId { get; set; }
    public string Name { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
  }
}