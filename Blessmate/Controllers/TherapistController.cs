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
        [Route("GetAll")]
        public async Task<IActionResult> GetTherapists(){

            return Ok(await _therapistsService.GetTherpists() );
        }

    }
}