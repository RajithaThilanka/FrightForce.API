using FrightForce.Application.Base;
using FrightForce.Domain.Documents;
using MediatR;

namespace FrightForce.Application.Features.Document.Docket.Queries;

public class GetAllDocumentsQuery : IQuery<Domain.Documents.Document>,ICacheableQuery
{ 
    public int DocketId { get; set; }
    public int DocumentId { get; set; }
    
    public string CacheKey => $"GetAllDocumentsQuery-{DocketId}-{DocumentId}";
    public TimeSpan? CacheExpiry => TimeSpan.FromMinutes(30);
}

public class GetAllDocumentsQueryHandler : IQueryHandler<GetAllDocumentsQuery, Domain.Documents.Document>
{
    private readonly IDocumentService _documentService;
    public GetAllDocumentsQueryHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    public async Task<Result<Domain.Documents.Document>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var document = await _documentService.GetDocumentsAsync(request.DocketId, request.DocumentId);
            return Result<Domain.Documents.Document>.Ok(document);
        }
        catch (Exception ex)
        {
            return Result<Domain.Documents.Document>.Fail(ex.Message);
        }
        
    }
}