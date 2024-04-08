using FrightForce.Application.Base;
using FrightForce.Domain.Documents;
using MediatR;

namespace FrightForce.Application.Features.Document.Docket.Commands;

public class UploadMultipleDocumentsCommand: ICommand<List<Domain.Documents.Document>>
{
    public int DocketId { get; set; }
    public List<UploadDto> fileList { get; set; } 
}

public class
    UploadMultipleDocumentsCommandHandler : ICommandHandler<UploadMultipleDocumentsCommand,
    List<Domain.Documents.Document>>
{
    public readonly IDocumentService _documentService;

    public UploadMultipleDocumentsCommandHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task<Result<List<Domain.Documents.Document>>> Handle(UploadMultipleDocumentsCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var document=await _documentService.UploadMultipleDocumentsAsync(request.fileList, request.DocketId);
            return Result<List<Domain.Documents.Document>>.Ok(document);
        }
        catch (Exception e)
        {
            return Result<List<Domain.Documents.Document>>.Fail(e.Message);
        }
        
    }
}