using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.BookDTOs;
using API.IServices;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute] int id)
        {
            try
            {
                return Ok(await _bookService.GetBookDetailsByIdAsync(id));
            }
            catch (Exception ex)
            {

                return NotFound("Error: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return NotFound("Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("mybook/{id}")]
        public async Task<IActionResult> GetAllMyBook(int id)
        {
            try
            {
                var books = await _bookService.GetMyAllBooksAsync(id);

                return Ok(books);
            }
            catch (Exception ex)
            {
                return NotFound("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
            try
            {
                await _bookService.CreateBookAsync(createBookDTO);
                return Ok("Success to create book");
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDTO updateBookDTO)
        {
            try
            {
                await _bookService.UpdateBookAsync(updateBookDTO);
                return Ok("Success to update book");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromBody] DeleteBookDTO deleteBookDTO)
        {
            try
            {
                await _bookService.DeleteBookAsync(deleteBookDTO);

                return Ok("Success to delete book");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}