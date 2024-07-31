using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.DTOs.AuthenDTOs;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AuthenService(ApplicationDbContext context) : IAuthenService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<User?> GetUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == loginModel.Username && x.Password == loginModel.Password) ?? throw new AuthenticationException("Incorrect username or password.");
                if (!user.IsActive)
                {
                    throw new InvalidOperationException("User is blocked.");
                }

                return user;
            }
            catch (AuthenticationException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while logging in.", ex);
            }
        }

        public async Task<User> RegisterAsync(User user)
        {
            try
            {
                if (await GetUserAsync(user.Username) != null)
                {
                    throw new DuplicateNameException("Username already exists");
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (DuplicateNameException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new ApplicationException("An error occurred while registering the user.", exception);
            }
        }
    }
}