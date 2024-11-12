namespace BookStore.Domain.Entities
{
    public interface IRole
    {
        Guid Id { get; set; }
        ERolesEnum Name { get; set; }
    }

    public class Role : IRole
    {
        public Guid Id { get; set; }

        public ERolesEnum Name { get; set; }
    }
}