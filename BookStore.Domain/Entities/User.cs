using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public interface IUserEntity: IBaseEntity
    {
        string Email { get; set; }
        Guid Id { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
    }

    public class User : BaseEntity, IUserEntity
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
