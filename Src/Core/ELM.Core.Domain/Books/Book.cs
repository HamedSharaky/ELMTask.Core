using Ardalis.GuardClauses;
using ELM.Core.Domain.Books.Inputs;
using ELM.Core.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace ELM.Core.Domain.Books
{
    public sealed class Book : AggregateRoot<long>
    {
        public BookInfo BookInfo { get; private set; }
        public DateTime LastModified { get; private set; }

        private Book()
        {
        }

        private static bool IsValidPublishDate(DateTime publishDate)
        {
            return publishDate >= DateTime.UtcNow;
        }

        public static Book Create(CreateBookInput input)
        {
            Guard.Against.NullOrWhiteSpace(input.BookTitle, nameof(input.BookTitle));
            Guard.Against.NullOrWhiteSpace(input.BookDescription, nameof(input.BookDescription));
            Guard.Against.NullOrWhiteSpace(input.Author, nameof(input.Author));
            Guard.Against.OutOfSQLDateRange(input.PublishDate, nameof(input.PublishDate));
            Guard.Against.InvalidInput(input.PublishDate, nameof(input.PublishDate), IsValidPublishDate);
            Guard.Against.NullOrWhiteSpace(input.CoverBase64, nameof(input.CoverBase64));

            return new Book
            {
                BookInfo = BookInfo.Create(new CreateBookInfoInput
                {
                    BookTitle = input.BookTitle,
                    BookDescription = input.BookDescription,
                    Author = input.Author,
                    PublishDate = input.PublishDate,
                    CoverBase64 = input.CoverBase64
                }),
                LastModified = DateTime.UtcNow
            };
        }

        public void Update(UpdateBookInput input)
        {
            Guard.Against.NullOrWhiteSpace(input.BookTitle, nameof(input.BookTitle));
            Guard.Against.NullOrWhiteSpace(input.BookDescription, nameof(input.BookDescription));
            Guard.Against.NullOrWhiteSpace(input.Author, nameof(input.Author));
            Guard.Against.OutOfSQLDateRange(input.PublishDate, nameof(input.PublishDate));
            Guard.Against.InvalidInput(input.PublishDate, nameof(input.PublishDate), IsValidPublishDate);
            Guard.Against.NullOrWhiteSpace(input.CoverBase64, nameof(input.CoverBase64));

            BookInfo = BookInfo.Create(new CreateBookInfoInput
            {
                BookTitle = input.BookTitle,
                BookDescription = input.BookDescription,
                Author = input.Author,
                PublishDate = input.PublishDate,
                CoverBase64 = input.CoverBase64
            });
            LastModified = DateTime.UtcNow;
        }
    }
}
