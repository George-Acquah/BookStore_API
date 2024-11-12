namespace BookStore.Application.Common
{
    public interface ITokens
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }

    public class Tokens : ITokens
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
