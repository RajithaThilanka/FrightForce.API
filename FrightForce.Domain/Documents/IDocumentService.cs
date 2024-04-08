using Microsoft.AspNetCore.Http;

namespace FrightForce.Domain.Documents;

public interface IDocumentService
{
    Task<Docket> CreateDocketAsync(int companyId, string name);
    Task<Docket> GetDocketAsync(int docketId, bool fetchDocuments);
    Task<Document> GetDocumentsAsync(int docketId, int documentId);
    Task<Document> CreateDocumentAsync(int docketId, int documentTypeId, string name);
    Task<Document> UploadFileAsync(int docketId,int documentId, IFormFile content, string? name, bool isRenaming);
    Task<Document> UploadNewDocumentAsync(int docketId, int documentTypeId, string name, IFormFile content);
    Task<List<Document>> UploadMultipleDocumentsAsync(List<UploadDto> files, int docketId);
    Task<DownloadDto> DownloadFileAsync(int docketId, int documentId);
    Task<Document?> GetDocumentByIdAsync(int id);
}