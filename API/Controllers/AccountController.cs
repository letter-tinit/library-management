using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.AccountDTO;
using API.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/accounts")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {

        private readonly IAccountService _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _accountService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            try
            {
                await _accountService.GetUserByIdAsync(id);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                await _accountService.CreateUserAsync(createUserDTO);

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            try
            {
                await _accountService.UpdateUserAsync(updateUserDTO);

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] int id)
        {
            try
            {
                await _accountService.DeleteUserAsync(id);
                return Ok("Delete user successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("" + ex.Message);
            }
        }
    }
}