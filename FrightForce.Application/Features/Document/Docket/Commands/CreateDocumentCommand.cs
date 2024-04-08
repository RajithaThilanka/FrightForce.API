using FrightForce.Application.Base;
using FrightForce.Domain.Documents;
using MediatR;

namespace FrightForce.Application.Features.Document.Docket.Commands;

public class CreateDocumentCommand: ICommand<Domain.Documents.Document>
{
    public string Name { get; set; }
    public int DocketId { get; set; }
    public int DocumentTypeId { get; set; } 
}

public class CreateDocumentCommandHandler : ICommandHandler<CreateDocumentCommand, Domain.Documents.Document>
{
    private readonly IDocumentService _documentService;

    public CreateDocumentCommandHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task <Result<Domain.Documents.Document>> Handle(CreateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var document =await _documentService.CreateDocumentAsync(request.DocketId, request.DocumentTypeId, request.Name);
            return Result<Domain.Documents.Document>.Ok(document);
        }
        catch (Exception ex)
        {
            return Result<Domain.Documents.Document>.Fail(ex.Message);
        }
    }
}