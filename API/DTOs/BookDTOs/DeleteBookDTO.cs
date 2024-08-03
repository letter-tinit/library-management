using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.BookDTOs
{
    public class DeleteBookDTO
    {
        public int BookId { get; set; }

        public int DeletedBy { get; set; }
    }
}