using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Dtos
{
    public class SafeUserDto: BaseEntity, ISafeUser
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
    }
}
