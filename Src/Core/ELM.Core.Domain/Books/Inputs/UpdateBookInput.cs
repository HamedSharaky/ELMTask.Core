namespace ELM.Core.Domain.Books.Inputs
{
    public sealed record UpdateBookInput
    {
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string CoverBase64 { get; set; }
    }
}
