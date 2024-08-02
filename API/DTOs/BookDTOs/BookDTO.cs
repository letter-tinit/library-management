using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.BookDTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfCopy { get; set; }
        public int NumberOfAvailableCopy { get; set; }
    }
}