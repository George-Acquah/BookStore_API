using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public interface IBook
    {
        List<string> Categories { get; set; }
        string Description { get; set; }
        Guid Id { get; set; }
        string Title { get; set; }
    }

    public class Book : BaseEntity, IBook
    {
        public Guid Id { get; set; }
        [Key]
        public required string Title { get; set; }

        public required string Description { get; set; }

        public List<string> Categories { get; set; } = [];

        public float Price { get; set; }

    }
}
