using System;
using System.Collections.Generic;

namespace API.Models;

public partial class BookCopy
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int CopyNumber { get; set; }

    public bool IsAvailable { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Book? Book { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
