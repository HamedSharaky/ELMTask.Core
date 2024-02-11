namespace ELM.Core.Domain.Common.Dtos.Cache
{
    public sealed record BookSearchCacheDto
    {
        public IEnumerable<long> BookIdz { get; set; }
    }
}