namespace CollaboratorsManagement.Common.Dtos.Pagination
{
    public record BasePaginationResponseDto
    {
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
    }
}