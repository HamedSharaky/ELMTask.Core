using Ardalis.GuardClauses;
using ELM.Core.Domain.Books.Inputs;
using ELM.Core.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace ELM.Core.Domain.Books
{
    public sealed class BookInfo
    {
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string CoverBase64 { get; set; }

        private BookInfo()
        {
        }
        
        internal static BookInfo Create(CreateBookInfoInput input)
        {
            return new BookInfo
            {
                BookTitle = input.BookTitle,
                BookDescription = input.BookDescription,
                Author = input.Author,
                PublishDate = input.PublishDate,
                CoverBase64 = input.CoverBase64
            };
        }
    }
}
