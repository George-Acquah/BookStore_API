namespace BookStore.Application.Dtos
{
    public class AddBookDto
    {
        public required string Title { get; set; }

        public required string Author { get; set; }

        public required string Description { get; set; }

        public required string BookImgUrl { get; set; }

        public required string FilePath { get; set; }

        public required List<string> Categories { get; set; }

        public float Price { get; set; }
    }
}
