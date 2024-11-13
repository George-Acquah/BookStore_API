namespace BookStore.Application.Dtos
{
    public interface IFileResponseDto
    {
        string? ErrorMessage { get; set; }
        string? FilePath { get; set; }
        bool Success { get; set; }
    }

    public class FileResponseDto : IFileResponseDto
    {
        public bool Success { get; set; }
        public string? FilePath { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
