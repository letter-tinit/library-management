using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.AccountDTO;
using API.Models;

namespace API.IServices
{
    public interface IAccountService
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> CreateUserAsync(CreateUserDTO createUserDTO);

        Task<User> UpdateUserAsync(UpdateUserDTO updateUserDTO);

        Task<User> DeleteUserAsync(int userId);
    }
}