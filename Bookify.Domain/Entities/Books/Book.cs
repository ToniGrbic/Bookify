using Bookify.Domain.Entities.Users;

namespace Bookify.Domain.Entities.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public DateTime? PublishedDate { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        
        // Navigation property
        public User? User { get; set; }
    }
}
