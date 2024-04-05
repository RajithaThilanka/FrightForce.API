using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrightForce.Infractructure.Persistence.Configurations.Document;

public class DocumentEntityTypeConfiguration: IEntityTypeConfiguration<Domain.Documents.Document>
{
      
    private const string TableName = "ff_documents";

    public void Configure(EntityTypeBuilder<Domain.Documents.Document> builder)
    {
        builder.ToTable(TableName, FrightForceDbContext.DefaultSchema);

        builder.HasKey(d => d.Id);
    }
}