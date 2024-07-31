using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.AuthenDTOs;
using API.Models;

namespace API.Mapper
{
    public static class AuthenMapper
    {
        public static User ToUserFromRegisterRequestDTO(this RegisterModel registerModel)
        {
            return new User
            {
                Username = registerModel.Username,
                Password = registerModel.Password,
                Email = registerModel.Email,
                Role = registerModel.Role,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };
        }
    }
}