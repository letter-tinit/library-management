using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Loan;
using API.Models;

namespace API.IServices
{
    public interface ILoanService
    {
        Task<Loan> LoanBookAsync(LoanDTO loanDTO);

        Task ReturnBookAsync(int loanId);
    }
}