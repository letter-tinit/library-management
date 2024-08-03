using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.AccountDTO;
using API.IServices;
using API.Mapper;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService(ApplicationDbContext context) : IAccountService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<User> CreateUserAsync(CreateUserDTO createUserDTO)
        {

            var user = createUserDTO.ToUserFromCreateUserDTO();
            try
            {
                if (await CheckUserExisted(user.Username, user.Email) != null)
                {
                    throw new Exception("Username already exists");
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> DeleteUserAsync(int userId)
        {
            try
            {
                var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found");

                if (userToDelete.Role.Equals("admin"))
                {
                    throw new Exception("Not allowed to delete");
                }

                userToDelete.IsActive = false;
                await _context.SaveChangesAsync();

                return userToDelete;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var users = await _context.Users.Where(u => u.IsActive && !u.Role.Equals("admin")).ToListAsync() ?? throw new Exception("Faild to get users");

                if (users.Count() == 0)
                {
                    throw new Exception("User not found");
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            try
            {

                var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateUserDTO.Id) ?? throw new Exception("User not found");

                if (await CheckUserExistedForUpdate(userToUpdate.Id, userToUpdate.Username, userToUpdate.Email) != null)
                {
                    throw new Exception("Username already exists");
                }

                userToUpdate.Username = updateUserDTO.Username;
                userToUpdate.Password = updateUserDTO.Password;
                userToUpdate.Email = updateUserDTO.Email;
                userToUpdate.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return userToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Utility methods
        private async Task<User?> CheckUserExisted(string username, string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
        }

        private async Task<User?> CheckUserExistedForUpdate(int userId, string username, string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id != userId && (u.Username == username || u.Email == username));
        }

    }
}