using FrightForce.Domain.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrightForce.Infractructure.Persistence.Document;

public class DocketEntityTypeConfiguration: IEntityTypeConfiguration<Docket>
{
    private const string TableName = "xb_dockets";
    public void Configure(EntityTypeBuilder<Docket> builder)
    {
        builder.ToTable("xb_dockets", FrightForceDbContext.DefaultSchema);
        builder.HasKey(d => d.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder
            .HasMany(d => d.Documents)
            .WithMany(d => d.Dockets)
            .UsingEntity<DocketDocument>(
                docketDocument =>
                    docketDocument.HasOne(d => d.Document)
                        .WithMany().HasForeignKey(d => d.DocumentId).IsRequired(false),
                docketDocument =>
                    docketDocument.HasOne(d => d.Docket)
                        .WithMany().HasForeignKey(d => d.DocketId).IsRequired(false)
            );
        
    } 
}