using System.Collections.Generic;

namespace DoctorsOffice.Models
{
  public class Specialty
  {
    public int SpecialtyId { get; set; }
    public string Name { get; set; }
    public List<DoctorSpecialty> JoinEntities { get;}
  }
}
