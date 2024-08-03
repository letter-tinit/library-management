using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.CategoryDTO
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}