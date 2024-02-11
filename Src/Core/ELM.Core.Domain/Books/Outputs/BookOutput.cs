namespace ELM.Core.Domain.Books.Outputs
{
    public sealed record BookOutput
    {
        public long BookId { get; init; }
        public DateTime LastModified { get; init; }
        public string BookTitle { get; init; }
        public string BookDescription { get; init; }
        public string Author { get; init; }
        public DateTime PublishDate { get; init; }
        public string CoverBase64 { get; init; }
    }
}
