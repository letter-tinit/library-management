using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.AuthenDTOs;
using API.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace API.IServices
{
    public interface IAuthenService
    {
        Task<User?> GetUserAsync(string username);

        Task<User> LoginAsync(LoginModel loginModel);

        Task<User> RegisterAsync(User user);
    }
}