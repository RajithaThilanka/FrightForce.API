using FrightForce.Domain.Base;

namespace FrightForce.Domain.Documents;

public interface IDocumentRepository: IRepository<Document, int>
{
    Task<Document> FetchDocumentAsync(int documentId);
    Task<Document> UpdateDocumentAsync(Document entity, bool shouldCommitTansaction);
    Task<IEnumerable<Document>> AddDocumentsRangeAsync(IEnumerable<Document> entities);
}