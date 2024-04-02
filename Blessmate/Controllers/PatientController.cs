using Microsoft.AspNetCore.Mvc;

namespace Blessmate;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{   

    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    [Route("GetTherapistes")]
    public async Task<IActionResult> GetTherapistes(){

        return Ok(await _patientService.GetTherpists());
    }

    [HttpGet]
    [Route("GetFiltredTherpists")]
    public async Task<IActionResult> GetFiltredTherpists([FromQuery] TherapistFilter filter){

        return Ok(await _patientService.GetFiltredTherpists(filter));
    }
    
    [HttpGet]
    [Route("GetTherpistsByLowestPrice")]
    public async Task<IActionResult> GetTherpistsByLowestPrice(){

        return Ok(await _patientService.GetTherpistsOrderBy(th => th.Price));
    }
    [HttpGet]
    [Route("GetTherpistsByHighestPrice")]
    public async Task<IActionResult> GetTherpistsByHighestPrice(){

        return Ok(await _patientService.GetTherpistsOrderByDesc(th => th.Price));
    }
    [HttpGet]
    [Route("GetTherpistsByLowestExperience")]
    public async Task<IActionResult> GetTherpistsByLowestExperience(){

        return Ok(await _patientService.GetTherpistsOrderBy(th => th.YearsExperience));
    }
    [HttpGet]
    [Route("GetTherpistsByHighestExperience")]
    public async Task<IActionResult> GetTherpistsByHighestExperience(){

        return Ok(await _patientService.GetTherpistsOrderByDesc(th => th.YearsExperience));
    }

    [HttpGet]
    [Route("GetTherpistsByAppoinment")]
    public async Task<IActionResult> GetTherpistsByAppoinment(){

        return Ok(await _patientService.GetTherpistsOrderByAppointment());
    }

    [HttpGet]
    [Route("GetAvailableAppointment")]
    public async Task<IActionResult> GetAvailableAppointment([FromQuery] int id){

        return Ok(await _patientService.GetAvailableAppointment(id));
    }

    [HttpPost]
    [Route("MakeAppointment")]
    public async Task<IActionResult> MakeAppointment(AppointmentDto dto){
        var result = await _patientService.MakeAppointment(dto);
        if(!result)
            return BadRequest();

        return Ok();
    }



}
