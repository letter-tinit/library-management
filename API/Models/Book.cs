using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public DateOnly PublishedDate { get; set; }

    public string Isbn { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    public virtual Category? Category { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
}
