using FrightForce.Application.Base;
using FrightForce.Domain.Documents;
using MediatR;

namespace FrightForce.Application.Features.Document.Docket.Queries;

public class GetDocumentQuery: IQuery<Domain.Documents.Document>
{
    public int Id { get; set; } 
}

public class GetDocumentQueryHandler : IQueryHandler<GetDocumentQuery, Domain.Documents.Document>
{
    private readonly IDocumentService _documentService;
    public GetDocumentQueryHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    public async Task<Result<Domain.Documents.Document>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        try
        {
           var result= await _documentService.GetDocumentByIdAsync(request.Id);
           return Result<Domain.Documents.Document>.Ok(result);
        }
        catch (Exception ex)
        {
            return Result<Domain.Documents.Document>.Fail(ex.Message);
        }
        
    }
}