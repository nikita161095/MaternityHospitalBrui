using MaternityHospitalBrui.Enums;
using System.ComponentModel.DataAnnotations;

namespace MaternityHospitalBrui.Features.UserProfile.Commands
{
    public class PatientCommand
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of Range or invalid date format")]
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }

    public class Name
    {
        public int Id { get; set; }
        public string? Use { get; set; }
        [Required]
        public string? Family { get; set; }
        public string? Given { get; set; }
    }
}