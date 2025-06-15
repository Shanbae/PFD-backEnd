using LoginAPI.BR;
using LoginAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers

{
    [Route("api/[Controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly RegistrationService _registrationService;
        public RegistrationController(RegistrationService registrationService)
        {

            _registrationService = registrationService;

        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Registration reg)
        {
            await _registrationService.AddUserAsync(reg);
            if (reg.Valid)
                return Ok("User added successfully.");
            else
                return BadRequest("Failed to add user.");
        }

        [HttpPost("FetchUser")]
        public IActionResult FetchUser([FromBody] string email)
        {
            _registrationService.FetchUserasync(email);
            return Ok();
        }
    }
}
