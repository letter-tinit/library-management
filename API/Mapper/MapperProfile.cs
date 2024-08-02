using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.AuthenDTOs;
using API.DTOs.BookDTOs;
using API.DTOs.Loan;
using API.Models;
using API.Utilities;

namespace API.Mapper
{
    public static class MapperProfile
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

        public static BookDTO ToBookDto(this Book book, int numberOfCopy, int numberOfAvailableCopy)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.AuthorName,
                CategoryName = book.Category.Name,
                IsActive = book.IsActive,
                NumberOfCopy = numberOfCopy,
                NumberOfAvailableCopy = numberOfAvailableCopy
            };
        }

        public static BookDetailsDTO ToBookDetailsDTO(this Book book, int numberOfCopy, int numberOfAvailableCopy)
        {
            return new BookDetailsDTO
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.AuthorName,
                CategoryName = book.Category.Name,
                CategoryId = book.CategoryId,
                PublishedDate = book.PublishedDate,
                Isbn = book.Isbn,
                CreatedBy = book.CreatedBy,
                NumberOfCopy = numberOfCopy,
                NumberOfAvailableCopy = numberOfAvailableCopy,
            };
        }

        public static Book ToBookFromCreateBook(this CreateBookDTO createBookDTO)
        {
            return new Book
            {
                Title = createBookDTO.Title,
                AuthorName = createBookDTO.AuthorName,
                CategoryId = createBookDTO.CategoryId,
                PublishedDate = Utility.ToDateOnly(createBookDTO.PublishedDate),
                Isbn = createBookDTO.Isbn,
                CreatedBy = createBookDTO.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };
        }

        public static Loan ToLoanFromLoanDTO(this LoanDTO loanDTO)
        {
            return new Loan
            {
                BookCopyId = loanDTO.BookCopyId,
                UserId = loanDTO.UserId,
                LoanDate = DateOnly.FromDateTime(DateTime.Now),
                ReturnDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };
        }
    }
}