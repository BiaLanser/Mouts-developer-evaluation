namespace Ambev.DeveloperEvaluation.Domain.DTOs
{
    public class PaginationDto<T>
    {
        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
