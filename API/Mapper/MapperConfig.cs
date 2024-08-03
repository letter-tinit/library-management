using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.BookDTOs;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Book, BookDetailsDTO>();
        }
    }
}