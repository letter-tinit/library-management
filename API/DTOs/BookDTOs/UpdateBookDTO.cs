using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.BookDTOs
{
    public class UpdateBookDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

        public int CategoryId { get; set; }

        public string PublishedDate { get; set; } = null!;

        public string Isbn { get; set; } = null!;

        public int UpdatedBy { get; set; }
    }
}