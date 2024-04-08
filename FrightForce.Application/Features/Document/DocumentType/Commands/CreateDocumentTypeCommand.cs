//
// using FrightForce.Application.Models.Document;
// using FrightForce.Domain.Documents;
// using Mapster;
// using MediatR;
//
// namespace FrightForce.Application.Features.Document.DocumentType.Commands;
//
// public record CreateDocumentTypeCommand(string Name, string Code) : IRequest<DocumentTypeVm>;
//
// public class CreateDocumentTypeCommandHandler : IRequestHandler<CreateDocumentTypeCommand, DocumentTypeVm>
// {
//     private readonly IDocumentTypeRepository _documentTypeRepository;
//
//     public CreateDocumentTypeCommandHandler(IDocumentTypeRepository documentTypeRepository)
//     {
//         _documentTypeRepository = documentTypeRepository;
//     }
//
//     public async Task<DocumentTypeVm> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
//     {
//     
//         
//         var documentType = DocumentType.Create(request.Name, request.Code);
//         var newDocumentType = await _documentTypeRepository.AddAsync(documentType);
//
//         return newDocumentType.Adapt<DocumentTypeVm>();                              
//     }
// }