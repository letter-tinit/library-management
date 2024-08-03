using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.CategoryDTO;
using API.Models;

namespace API.IServices
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();

        Task<Category> CreateCategoryAsync(string categoryName);

        Task<Category> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);

        Task<Category> DeleteCategoryAsync(int categoryId);
    }
}