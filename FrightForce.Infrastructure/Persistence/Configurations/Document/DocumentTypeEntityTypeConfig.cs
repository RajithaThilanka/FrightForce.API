using FrightForce.Domain.Documents;
using FrightForce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrightForce.Infractructure.Persistence.Configurations.Document;

public class DocumentTypeEntityTypeConfig: IEntityTypeConfiguration<DocumentType>
{
    private const string TableName = "ff_document_types";

    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable(TableName, FrightForceDbContext.DefaultSchema);
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.Code).IsUnique(true);
    }
    
}