using Domain.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestVM model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterVM model)
        {

            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleVM model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }
    }
}
