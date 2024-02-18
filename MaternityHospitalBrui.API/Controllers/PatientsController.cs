using MaternityHospitalBrui.Features;
using MaternityHospitalBrui.Features.UserProfile.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MaternityHospitalBrui.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientFeature _patientFeature;
        public PatientsController(IPatientFeature patientFeature)
        {
            _patientFeature = patientFeature;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientCommand model)
        {
            var response = await _patientFeature.Create(model);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _patientFeature.GetById(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _patientFeature.GetAll();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(PatientCommand model, int id)
        {
            var response = await _patientFeature.Update(model, id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _patientFeature.Delete(id);
            return Ok();
        }

        [HttpGet("Search/{date}")]
        public async Task<IActionResult> DateSearch(string date)
        {
            var response = await _patientFeature.Search(date);
            return Ok(response);
        }

    }
}
