using FrightForce.Domain.Documents;
using FrightForce.Infractructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FrightForce.Infrastructure.Persistence;

public class DocumentRepository: BaseEfCoreRepository<Document, int>,IDocumentRepository
{
    
    private new readonly FrightForceDbContext _context;
    
    public DocumentRepository(FrightForceDbContext context):base(context)
    {
        _context = context;
    }
    public async Task<Document> FetchDocumentAsync(int documentId)
    {
        return await _context.Documents
            .FirstAsync(d => d.Id == documentId);
    }
    public async Task<Document> UpdateDocumentAsync(Document entity,bool shouldCommitTansaction)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        if(shouldCommitTansaction)
        {
            await _context.RestartTransactionAsync();
        }
        return entity;
    }

    public async Task<IEnumerable<Document>> AddDocumentsRangeAsync(IEnumerable<Document> entities)
    {
        _context.Set<Document>().AddRange(entities);
        await _context.SaveChangesAsync();
        await _context.RestartTransactionAsync();
        return entities;
    }
}