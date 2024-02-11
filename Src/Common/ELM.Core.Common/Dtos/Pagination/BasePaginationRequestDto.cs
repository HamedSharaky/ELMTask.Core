namespace ELM.Core.Common.Dtos.Pagination
{
    public record BasePaginationRequestDto
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}