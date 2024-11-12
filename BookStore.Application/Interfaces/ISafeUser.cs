using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface ISafeUser: IBaseEntity
    {
        Guid Id { get; }
        string Email { get; set; }

        string UserRole { get; set; }
        string UserName { get; set; }
    }
}
