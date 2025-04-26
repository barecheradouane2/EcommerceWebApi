namespace EcommerceWeb.Api.Models.DTO
{
    public class PagedResultDTO <T> where T : class
    {

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; } = new();
    }
}
