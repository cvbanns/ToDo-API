using Microsoft.AspNetCore.Mvc;
using ToDoAuth.DTO;
using ToDoAuth.Services;

namespace ToDoAuth.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {        
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(RegisterDTO registerDTO)
        {
            return Ok(await _authService.Register(registerDTO));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDTO loginDTO)
        {
            return Ok(await _authService.Login(loginDTO));
        }

        


    }
}