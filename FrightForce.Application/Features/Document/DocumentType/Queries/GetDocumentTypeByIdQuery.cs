using FrightForce.Application.Common.Exceptions;
using FrightForce.Application.Models.Document;
using FrightForce.Domain.Documents;
using Mapster;
using MediatR;

namespace FrightForce.Application.Features.Document.DocumentType.Queries;

public record GetDocumentTypeById(int Id) : IRequest<DocumentTypeVm?>;

public class GetDocumentTypeByIdHandler : IRequestHandler<GetDocumentTypeById, DocumentTypeVm?>
{
    private readonly IDocumentTypeRepository _documentTypeRepository;

    public GetDocumentTypeByIdHandler(IDocumentTypeRepository documentTypeRepository)
    {
        _documentTypeRepository = documentTypeRepository;
    }

    public async Task<DocumentTypeVm?> Handle(GetDocumentTypeById request, CancellationToken cancellationToken)
    {
        var documentType = await _documentTypeRepository.FindByIdAsync(request.Id);
        
        return documentType.Adapt<DocumentTypeVm>();
    }
}