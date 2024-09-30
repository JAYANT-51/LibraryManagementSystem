using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuedBooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public IssuedBooksController(LibraryContext context)
        {
            _context = context;
        }

       
        [HttpPost("IssueBook")]
        public async Task<ActionResult> IssueBook(int bookId, int userId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.AvailableCopies <= 0)
            {
                return BadRequest("Book is not available.");
            }

            var issuedBook = new IssuedBook
            {
                BookID = bookId,
                UserID = userId,
                IssueDate = DateTime.Now
            };

            book.AvailableCopies--;

            _context.IssuedBooks.Add(issuedBook);
            await _context.SaveChangesAsync();

            return Ok("Book issued successfully.");
        }

       
        [HttpPost("ReturnBook")]
        public async Task<ActionResult> ReturnBook(int issueId)
        {
            var issuedBook = await _context.IssuedBooks.FindAsync(issueId);
            if (issuedBook == null || issuedBook.ReturnDate != null)
            {
                return BadRequest("Invalid issued book.");
            }

            issuedBook.ReturnDate = DateTime.Now;

            var book = await _context.Books.FindAsync(issuedBook.BookID);
            if (book != null)
            {
                book.AvailableCopies++;
            }

            await _context.SaveChangesAsync();

            return Ok("Book returned successfully.");
        }

        [HttpGet("TotalIssued")]
        public async Task<ActionResult<int>> GetTotalBooksIssued()
        {
            return await _context.IssuedBooks.CountAsync();
        }

       
        [HttpGet("AvailableCopies/{bookId}")]
        public async Task<ActionResult<int>> GetAvailableCopies(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            return book.AvailableCopies;
        }
    }
}
