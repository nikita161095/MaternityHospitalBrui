using MaternityHospitalBrui.Enums;
using MaternityHospitalBrui.States.Interfaces;

namespace MaternityHospitalBrui.States
{
    public class PatientState : IPatientState
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }
}
