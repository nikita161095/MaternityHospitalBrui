using MaternityHospitalBrui.Enums;

namespace MaternityHospitalBrui.States.Interfaces
{
    public interface IPatientState
    {
        int Id { get; set; }
        int NameId { get; set; }
        Gender Gender { get; set; }
        DateTime BirthDate { get; set; }
        bool Active { get; set; }
    }
}
