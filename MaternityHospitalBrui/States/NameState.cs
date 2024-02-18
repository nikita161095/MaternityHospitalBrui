using MaternityHospitalBrui.States.Interfaces;

namespace MaternityHospitalBrui.States
{
    public class NameState : INameState
    {
        public int Id { get; set; }
        public string? Use { get; set; }
        public string? Family { get; set; }
        public string? Given { get; set; }
    }
}