using FrightForce.Application.Base;
using FrightForce.Domain.Documents;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FrightForce.Application.Features.Document.Docket.Commands;

public class UploadDocumentCommand: ICommand<Domain.Documents.Document>
{
    public int DocketId { get; set; }
    public int DocumentId { get; set; }
    public IFormFile Content { get; set; }
    public string? Name { get; set; }
    public bool IsRenaming { get; set; } 
}

public class UploadDocumentCommandHandler : ICommandHandler<UploadDocumentCommand, Domain.Documents.Document>
{
    private readonly IDocumentService _documentService;
    public UploadDocumentCommandHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    public async Task<Result<Domain.Documents.Document>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var docuemtn= await _documentService.UploadFileAsync(request.DocketId, request.DocumentId, request.Content, request.Name, request.IsRenaming);
            return Result<Domain.Documents.Document>.Ok(docuemtn);
        }
        catch (Exception e)
        {

            return Result<Domain.Documents.Document>.Fail(e.Message);
        }
        
    }
}