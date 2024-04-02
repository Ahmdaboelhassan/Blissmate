using Microsoft.AspNetCore.Mvc;

namespace Blessmate;

[ApiController]
[Route("[controller]")]
public class DashbourdController : ControllerBase
{   
    private readonly IDashbourdService _dashbourdService;
    public DashbourdController(IDashbourdService dashbourdService)
    {
        _dashbourdService = dashbourdService;
    }

    [HttpGet]
    [Route("GetUnConfirmedTherapists")]
    public async Task<IActionResult> GetUnConfirmedTherapists(){
        return Ok(await _dashbourdService.GetUnConfirmedTherapists());
    }    
    
    [HttpGet]
    [Route("GetApplicationStatictics")]
    public async Task<IActionResult> GetApplicationStatictics(){
        return Ok(await _dashbourdService.GetApplicationStatictics());
    }    
    
    [HttpPost]
    [Route("ConfirmTherapist/{id}")]
    public async Task<IActionResult> ConfirmTherapist(int id){
        var result = await _dashbourdService.ConfirmTherapist(id);

        if(!result)
            return BadRequest();

        return Ok();
    }    
    
    [HttpPost]
    [Route("DeleteTherpist/{id}")]
    public async Task<IActionResult> DeleteTherpist(int id){
        var result = await _dashbourdService.DeleteTherpist(id);

        if(!result)
            return BadRequest();

        return Ok();
    }    
}
