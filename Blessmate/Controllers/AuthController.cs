using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Blessmate.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;

    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register( Register model){
        
        bool s = ModelState.IsValid;

        var response = await _authService.RegisterAsync(model);
        
        if(!response.isAuth){
            return BadRequest(response.messages);
        }

        return Ok(response);
    
    }

   [HttpPost]
   [Route("Login")]
   public async Task<IActionResult> Login(Login model){

        var response = await _authService.LoginAsync(model);

        if(!response.isAuth)
            return BadRequest(response.messages);

        return Ok(response);
   }
}