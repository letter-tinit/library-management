using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.CategoryDTO;
using API.IServices;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class CategoryService(ApplicationDbContext context) : ICategoryService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Category> CreateCategoryAsync(string categoryName)
        {

            try
            {
                if (IsCategoryNameExisted(categoryName))
                {
                    throw new Exception("Category name already existed.");
                }

                var newCategory = new Category
                {
                    Name = categoryName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsActive = true
                };

                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();

                return newCategory;

            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<Category> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId) ?? throw new Exception("No category found to delete");

                categoryToDelete.UpdatedAt = DateTime.Now;
                categoryToDelete.IsActive = false;
                await _context.SaveChangesAsync();

                return categoryToDelete;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.Where(c => c.IsActive).ToListAsync();


                if (categories == null || categories.Count == 0)
                {
                    throw new Exception("No categories were found");
                }

                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            try
            {
                var categoryToUpdate = await _context.Categories.FindAsync(updateCategoryDTO.Id) ?? throw new Exception("Category not found");
                categoryToUpdate.Name = updateCategoryDTO.CategoryName;
                categoryToUpdate.UpdatedAt = DateTime.Now;

                if (IsCategoryNameExisted(updateCategoryDTO.CategoryName))
                {
                    throw new Exception("Category name already existed.");
                }

                await _context.SaveChangesAsync();

                return categoryToUpdate;

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        // Utility methods

        private bool IsCategoryNameExisted(string categoryName)
        {
            bool exists = _context.Categories
                                        .Any(c => c.Name == categoryName && c.IsActive == true);

            return exists;
        }
    }
}