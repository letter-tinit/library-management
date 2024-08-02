using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Loan;
using API.IServices;
using API.Mapper;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LoanService(ApplicationDbContext context) : ILoanService
    {

        private readonly ApplicationDbContext _context = context;

        public async Task<Loan> LoanBookAsync(LoanDTO loanDTO)
        {
            try
            {
                // Find the book to loan
                var bookToLoan = await FindBookCopyByIdAsync(loanDTO.BookCopyId) ?? throw new Exception("Book Not Found");


                // check if the book is borrowed
                if (!bookToLoan.IsAvailable)
                {
                    throw new Exception("Book is borrowed");
                }

                // Create a new loan
                Loan loan = loanDTO.ToLoanFromLoanDTO();

                // Update the book copy status
                bookToLoan.IsAvailable = false;

                // Save change in database
                await _context.Loans.AddAsync(loan);
                await _context.SaveChangesAsync();

                return loan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task ReturnBookAsync(int loanId)
        {
            try
            {
                var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == loanId && l.IsActive == true) ?? throw new Exception("Loan not found or book have been returned");

                // Update the book copy status
                var bookCopyId = loan.BookCopyId ?? throw new Exception("Input is not available");
                var bookCopy = await FindBookCopyByIdAsync(bookCopyId) ?? throw new Exception("Book copy not found");
                bookCopy.IsAvailable = true;
                loan.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Utilities methods
        private async Task<BookCopy?> FindBookCopyByIdAsync(int bookCopyId)
        {
            return await _context.BookCopies.FindAsync(bookCopyId);
        }
    }
}