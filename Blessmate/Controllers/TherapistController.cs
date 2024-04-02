using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Blessmate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TherapistController : ControllerBase
    {
        private readonly ITherapistsService _therapistsService;
        public TherapistController(ITherapistsService therapistsService)
        {
            _therapistsService = therapistsService;
        }
        
        [HttpGet]
        [Route("GetTherapistHistory")]
        public async Task<IActionResult> GetTherapistHistory(int id){

            return Ok(await _therapistsService.GetAppointmentHistory(id));
        }
        [HttpGet]
        [Route("GetTherapistPatient")]
        public async Task<IActionResult> GetTherapistPatient(int id){

            return Ok(await _therapistsService.GetTherapistPatient(id));
        }
        [HttpGet]
        [Route("GetLastTherapistPatient")]
        public async Task<IActionResult> GetLastTherapistPatient(int id){

            return Ok(await _therapistsService.GetLastTherapistPatient(id));
        }

        [HttpPost]
        [Route("TherapistProfile")]
        public async Task<IActionResult> TherapistProfile([FromQuery] int id ,
             [FromForm] TherapistProfile profile){

            var result = await _therapistsService.AddTherapistProfile(id, profile);

            if(!result) return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("TherapistCertificate")]
        public async Task<IActionResult> TherapistCertificate([FromQuery] int id ,
             [FromForm] IFormFile Certificate){

            var result = await _therapistsService.AddTherapistCertificate(id, Certificate);

            if(!result) return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("AddTherapistAppointment")]
        public async Task<IActionResult> AddTherapistAppointment(AppointmentDto dto){

            var result = await _therapistsService.AddTherapistAppointment(dto);

            if(!result) return BadRequest();

            return Ok();
        }


    }
}