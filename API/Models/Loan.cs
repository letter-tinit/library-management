using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Loan
{
    public int Id { get; set; }

    public int? BookCopyId { get; set; }

    public int? UserId { get; set; }

    public DateOnly LoanDate { get; set; }

    public DateOnly ReturnDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual BookCopy? BookCopy { get; set; }

    public virtual User? User { get; set; }
}
