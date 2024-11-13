using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public interface IBook: IBaseEntity
    {
        User? AddedBy { get; set; }
        Guid AddedById { get; set; }
        string Author { get; set; }
        List<string> Categories { get; set; }
        string Description { get; set; }
        Guid Id { get; set; }
        string BookImgUrl { get; set; }
        string FilePath { get; set; }
        float Price { get; set; }
        string Title { get; set; }
    }

    public class Book : BaseEntity, IBook
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid AddedById { get; set; }
        public User? AddedBy { get; set; }
        [Key]
        public required string Title { get; set; }

        public required string Author { get; set; }

        public required string Description { get; set; }

        public required string BookImgUrl { get; set; }

        public required string FilePath { get; set; }

        public List<string> Categories { get; set; } = [];

        public float Price { get; set; }

    }
}
