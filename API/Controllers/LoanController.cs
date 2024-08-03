using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Loan;
using API.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/loans")]
    public class LoanController(ILoanService loanService) : ControllerBase
    {

        private readonly ILoanService _loanService = loanService;

        [HttpPost]
        public async Task<IActionResult> LoanBook([FromBody] LoanDTO loanDTO)
        {
            try
            {
                var loanBook = await _loanService.LoanBookAsync(loanDTO);

                return Ok("Loan Book Success:\nReturn Date: " + loanBook.ReturnDate);
            }
            catch (Exception ex)
            {

                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ReturnBook([FromBody] int loanId)
        {
            try
            {
                await _loanService.ReturnBookAsync(loanId);

                return Ok("Book returned successfully");
            }
            catch (Exception ex)
            {

                return BadRequest("Failed to return the book: " + ex.Message);
            }
        }
    }
}