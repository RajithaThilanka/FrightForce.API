using FrightForce.Application.Base;
using FrightForce.Domain.Documents;
using MediatR;

namespace FrightForce.Application.Features.Document.Docket.Queries;

public class DownloadDocumentQuery: IQuery<DownloadDto>
{
    public int DocketId { get; set; }
    public int DocumentId { get; set; }
}

public class DownloadDocumentQueryHandler : IQueryHandler<DownloadDocumentQuery, DownloadDto>
{
    private readonly IDocumentService _documentService;

    public DownloadDocumentQueryHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task<Result<DownloadDto>> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _documentService.DownloadFileAsync(request.DocketId, request.DocumentId);
            return Result<DownloadDto>.Ok(result);
        }
        catch (Exception e)
        {
            return Result<DownloadDto>.Fail(e.Message);
        }
    }
}