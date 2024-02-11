using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ELM.Core.Domain.Books;
using Newtonsoft.Json;

namespace ELM.Core.Persistence.Domain.Books
{
    sealed class VwBookConfiguration : IEntityTypeConfiguration<VwBook>
    {
        public void Configure(EntityTypeBuilder<VwBook> builder)
        {
            builder.ToView(ELMContext.Views.Book, ELMContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);
        }
    }

}
