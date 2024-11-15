namespace BookStore.Application.Dtos
{
    public class UpdateBookDto
    {
        public string Title { get; set; }= String.Empty;

        public string Author { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string BookImgUrl { get; set; } = String.Empty;

        public string FilePath { get; set; } = String.Empty;

        public List<string>? Categories { get; set; }

        public required float Price { get; set; }
    }
}
