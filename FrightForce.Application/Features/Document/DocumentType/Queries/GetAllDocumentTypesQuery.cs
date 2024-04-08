using FrightForce.Application.Models.Document;
using FrightForce.Domain.Documents;
using MapsterMapper;
using MediatR;

namespace FrightForce.Application.Features.Document.DocumentType.Queries;

public record GetAllDocumentTypesQuery() : IRequest<List<DocumentTypeVm>>, ICacheableQuery
{
    public string CacheKey => "GetAllDocumentTypesQuery";
    public TimeSpan? CacheExpiry => TimeSpan.FromMinutes(30);
}


public class GetAllDocumentTypesQueryHandler : IRequestHandler<GetAllDocumentTypesQuery, List<DocumentTypeVm>>
{
    private readonly IDocumentTypeRepository _documentTypeRepository;
    private readonly IMapper _mapper;

    public GetAllDocumentTypesQueryHandler(IDocumentTypeRepository documentTypeRepository, IMapper mapper)
    {
        _documentTypeRepository = documentTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<DocumentTypeVm>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        var documentTypes = await _documentTypeRepository.FindAllAsync();
        return _mapper.Map<List<DocumentTypeVm>>(documentTypes);
    }
}