using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using API.DTOs.AuthenDTOs;
using API.IServices;
using API.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/authen")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        IAuthenService _services;

        public AuthenController(IAuthenService services)
        {
            _services = services;
        }
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {

            try
            {
                var user = await _services.LoginAsync(loginModel);
                return Ok(user.Role);
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized("Error: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        // [HttpPost("/register")]
        // public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        // {
        //     var newUser = registerModel.ToUserFromRegisterRequestDTO();

        //     try
        //     {
        //         await _services.RegisterAsync(newUser);
        //         return Ok("User registered successfully");
        //     }
        //     catch (DuplicateNameException ex)
        //     {
        //         return Unauthorized("Error: " + ex.Message);
        //     }
        //     catch (Exception exception)
        //     {
        //         return BadRequest("Error: " + exception.Message);
        //     }
        // }
    }
}