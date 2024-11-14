namespace BookStore.Domain.Entities
{
    public interface IBookCategory
    {
        Guid Id { get; set; }
        string Name { get; set; }

        // Many-to-many navigation back to books
        public List<Book>? Books { get; set; }
    }

    public class BookCategory : IBookCategory
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        // Many-to-many navigation back to books
        public List<Book>? Books { get; set; }
    }
}
