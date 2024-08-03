using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.BookDTOs
{
    public class BookDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public int? CategoryId { get; set; }

        public DateOnly? PublishedDate { get; set; }

        public string? Isbn { get; set; }

        public int? CreatedBy { get; set; }

        public int NumberOfCopy { get; set; }

        public int NumberOfAvailableCopy { get; set; }

    }
}