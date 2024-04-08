using FrightForce.Domain.Documents;
using FrightForce.Infractructure.Persistence.Repositories;
using FrightForce.Infrastructure.Persistence;

namespace FrightForce.Infractructure.Persistence;

public class DocumentTypeRepository: BaseEfCoreRepository<DocumentType, int>, IDocumentTypeRepository
{
    private readonly FrightForceDbContext _context;
    public DocumentTypeRepository(FrightForceDbContext context):base(context)
    {
        _context = context;
    }
    
}