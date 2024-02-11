using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ELM.Core.Domain.Books;
using Newtonsoft.Json;

namespace ELM.Core.Persistence.Domain.Books
{
    sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book", ELMContext.DEFAULT_SCHEMA);

            builder.HasKey(e => e.Id).HasName("PK_Book");
            builder.Property(e => e.Id).HasColumnName("BookId");

            builder.Property(e => e.BookInfo).HasConversion(
                v => JsonConvert.SerializeObject(v, Formatting.Indented),
                v => JsonConvert.DeserializeObject<BookInfo>(v)
            );

            builder.Property(b => b.LastModified)
                .IsRequired()
                .HasColumnType("datetime2(7)");

            builder.HasIndex(e => e.LastModified, "IX_Book_LastModified");
        }
    }

}
