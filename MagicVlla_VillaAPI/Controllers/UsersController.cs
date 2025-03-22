using MagicVlla_VillaAPI.Models;
using MagicVlla_VillaAPI.Models.Dto;
using MagicVlla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVlla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepository, APIResponse response)
        {
            _userRepository = userRepository;
            this._response = response;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token)) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMassages.Add("Invalid Login Attempt: UserID or Password is not correct" );
                return BadRequest(_response.ErrorMassages);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result= loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserNameUnique = _userRepository.IsUniqueUser(model.userName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMassages.Add("Invalid  Attempt: User altready exist");
                return BadRequest();

            }
            var user = await _userRepository.Register(model);
            if (user == null) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMassages.Add("Invalid  Attempt: User Loggin error");
                return BadRequest(_response.ErrorMassages);

            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);

        }
    }
}
