using BookStore.Domain.Entities;

namespace BookStore.Application.Dtos
{
    public interface IBookResponseDto: IBaseEntity
    {
        string AddedBy { get; set; }
        string Author { get; set; }
        string BookFilePath { get; set; }
        string BookImgUrl { get; set; }
        List<string> Categories { get; set; }
        string Description { get; set; }
        string Id { get; set; }
        float Price { get; set; }
        string Title { get; set; }
    }

    public class BookResponseDto :BaseEntity, IBookResponseDto
    {
        public required string Id { get; set; }
        public required string AddedBy { get; set; }
        public required string Title { get; set; }

        public required string Author { get; set; }

        public required string Description { get; set; }

        public required string BookImgUrl { get; set; }

        public required string BookFilePath { get; set; }
        public required List<string> Categories { get; set; }
        public float Price { get; set; }
    }
}
