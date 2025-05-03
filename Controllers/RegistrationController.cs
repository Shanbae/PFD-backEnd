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
        public IActionResult AddUser([FromBody] Registration reg)
        {
            _registrationService.AddUserAsync(reg);
            return Ok();
        }

        [HttpPost("FetchUser")]
        public IActionResult FetchUser([FromBody] string email)
        {
            _registrationService.FetchUserasync(email);
            return Ok();
        }
    }
}
