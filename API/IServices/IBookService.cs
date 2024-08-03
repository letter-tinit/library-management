using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.BookDTOs;
using API.Models;

namespace API.IServices
{
    public interface IBookService
    {
        Task<BookDetailsDTO?> GetBookDetailsByIdAsync(int id);

        Task<List<BookDTO>> GetAllBooksAsync();

        Task<List<BookDTO>> GetMyAllBooksAsync(int ownerId);

        Task<Book> CreateBookAsync(CreateBookDTO createBookDTO);

        Task CreateBookCopiesAsync(List<BookCopy> bookCopies);

        Task<Book> UpdateBookAsync(UpdateBookDTO updateBookDTO);

        Task<Book> DeleteBookAsync(DeleteBookDTO deleteBookDTO);
    }
}