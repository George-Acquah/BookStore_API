using BookStore.Application.Common;
using BookStore.Domain;

namespace BookStore.Application.Dtos
{
    public interface ILoginResponseDto
    {
        ITokens Tokens { get; set; }

        string UserRole { get; set; }
        string UserName { get; set; }
    }

    public class LoginResponseDto : ILoginResponseDto
    {
        public required string UserName { get; set; }

        public required string UserRole { get; set; }
        public required ITokens Tokens { get; set; }
    }
}
