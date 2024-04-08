using FrightForce.Domain.Documents;
using FrightForce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrightForce.Infractructure.Persistence.Configurations.Document;

public class DocketDocumentEntityTypeConfiguration : IEntityTypeConfiguration<DocketDocument>
{
    
    private const string TableName = "ff_docket_documents";

    public void Configure(EntityTypeBuilder<DocketDocument> builder)
    {
        builder.ToTable(TableName, FrightForceDbContext.DefaultSchema);

    }
}