namespace BookStore.Application.Dtos
{
    public interface IPaginationMetaDto<T>
    {
        List<T> Items { get; set; }
        int CurrentPage { get; set; }
        int ItemsPerPage { get; set; }
        int TotalItems { get; set; }
        int TotalPages { get; set; }
    }

    public class PaginationMetaDto<T> : IPaginationMetaDto<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
