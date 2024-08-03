using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.BookDTOs;
using API.IServices;
using API.Mapper;
using API.Models;
using API.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class BookService(ApplicationDbContext context, IMapper mapper) : IBookService
    {
        private readonly ApplicationDbContext _context = context;

        private readonly IMapper _mapper = mapper;

        public async Task<Book> CreateBookAsync(CreateBookDTO createBookDTO)
        {
            try
            {
                // Convert DTO to Book entity
                var newBook = createBookDTO.ToBookFromCreateBook();

                // Add book and save changes
                await _context.Books.AddAsync(newBook);
                await _context.SaveChangesAsync();

                return newBook;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating book.", ex);
            }
        }

        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            try
            {
                // Get all books
                var books = await _context.Books
                                .Include(b => b.Category)
                                .Where(b => b.IsActive)
                                .ToListAsync();

                var bookDtos = new List<BookDTO>();

                foreach (var book in books)
                {
                    var numberOfCopy = await CalculateNumberOfCopyAsync(book);

                    var numberOfAvailableCopy = await CalculateNumberOfAvailabeCopyAsync(book);

                    bookDtos.Add(book.ToBookDto(numberOfCopy, numberOfAvailableCopy));
                }

                if (bookDtos.Count == 0)
                {
                    throw new Exception("No books found.");
                }

                return bookDtos;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<BookDetailsDTO?> GetBookDetailsByIdAsync(int id)
        {

            // GetBook by using Mapper
            // var book = await _context.Books
            //     .Include(c => c.Category)
            //     .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);

            // return _mapper.Map<BookDetailsDTO>(book);
            try
            {
                // Query book with IsActive, Throw exception when occur
                var book = await FindBookByIdAsync(id) ?? throw new Exception("No books matched.");

                var numberOfCopy = await CalculateNumberOfCopyAsync(book);

                var numberOfAvailableCopy = await CalculateNumberOfAvailabeCopyAsync(book);

                return book.ToBookDetailsDTO(numberOfCopy, numberOfAvailableCopy);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookDTO>> GetMyAllBooksAsync(int ownerId)
        {
            try
            {
                // Get all my books
                var books = await _context.Books
                                .Include(b => b.Category)
                                .Where(b => b.IsActive)
                                .Where(b => b.Id == ownerId)
                                .ToListAsync();

                var bookDtos = new List<BookDTO>();

                foreach (var book in books)
                {
                    var numberOfCopy = await CalculateNumberOfCopyAsync(book);

                    var numberOfAvailableCopy = await CalculateNumberOfAvailabeCopyAsync(book);

                    bookDtos.Add(book.ToBookDto(numberOfCopy, numberOfAvailableCopy));
                }

                if (bookDtos.Count == 0)
                {
                    throw new Exception("Your book has no");
                }

                return bookDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> UpdateBookAsync(UpdateBookDTO updateBookDTO)
        {
            try
            {
                var bookToUpdate = await FindBookByIdAsync(updateBookDTO.Id) ?? throw new InvalidOperationException("Book Not Found");

                if (bookToUpdate.CreatedBy != updateBookDTO.UpdatedBy)
                {
                    throw new InvalidOperationException("You are not the owner of this book.");
                }

                // Convert DTO to Book entity
                bookToUpdate.Title = updateBookDTO.Title;
                bookToUpdate.AuthorName = updateBookDTO.AuthorName;
                bookToUpdate.CategoryId = updateBookDTO.CategoryId;
                bookToUpdate.PublishedDate = Utility.ToDateOnly(updateBookDTO.PublishedDate);
                bookToUpdate.Isbn = updateBookDTO.Isbn;
                bookToUpdate.UpdatedAt = DateTime.Now;

                // Add book and save changes
                _context.Books.Update(bookToUpdate);
                await _context.SaveChangesAsync();

                return bookToUpdate;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> DeleteBookAsync(DeleteBookDTO deleteBookDTO)
        {

            try
            {
                var bookToDelete = await FindBookByIdAsync(deleteBookDTO.BookId) ?? throw new Exception("No books matched.");

                if (bookToDelete.CreatedBy != deleteBookDTO.DeletedBy)
                {
                    throw new InvalidOperationException("You are not the owner of this book.");
                }

                bookToDelete.IsActive = false;
                bookToDelete.UpdatedAt = DateTime.Now;
                _context.Books.Update(bookToDelete);
                await _context.SaveChangesAsync();

                return bookToDelete;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting book.", ex);
            }

        }

        // Utility methods
        private async Task<int> CalculateNumberOfCopyAsync(Book book)
        {
            return await _context.BookCopies.CountAsync(copy => copy.BookId == book.Id);
        }

        private async Task<int> CalculateNumberOfAvailabeCopyAsync(Book book)
        {
            return await _context.BookCopies.CountAsync(copy => copy.BookId == book.Id && copy.IsAvailable);
        }

        public async Task CreateBookCopiesAsync(List<BookCopy> bookCopies)
        {

            try
            {
                BookCopy bookCopy = await _context.BookCopies.OrderByDescending(copy => copy.CopyNumber).FirstOrDefaultAsync() ?? throw new ArgumentException("Failed to find book for create book copies");

                int maxNumber = bookCopy.CopyNumber;

                foreach (var copy in bookCopies)
                {
                    copy.CopyNumber = maxNumber + 1;
                    maxNumber++;
                }

                await _context.BookCopies.AddRangeAsync(bookCopies);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task<Book?> FindBookByIdAsync(int id)
        {

            var book = await _context.Books
                .Include(c => c.Category)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);

            return book;

        }
    }
}