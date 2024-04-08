using FrightForce.Application.Base;
using FrightForce.Domain.Documents;
using MediatR;

namespace FrightForce.Application.Features.Document.Docket.Queries;

public class GetDocketQuery: IQuery<Domain.Documents.Docket?>
{
    public int DocketId { get; set; }
    public bool FetchDocuments { get; set; }  
    public class GetDocketQueryHandler : IQueryHandler<GetDocketQuery, Domain.Documents.Docket?>
    {
        private IDocumentService _documentService;
        public GetDocketQueryHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<Result<Domain.Documents.Docket?>> Handle(GetDocketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result= await _documentService.GetDocketAsync(request.DocketId, request.FetchDocuments);
                return Result<Domain.Documents.Docket>.Ok(result);
            }
            catch (Exception ex)
            {
                return Result<Domain.Documents.Docket>.Fail(ex.Message);
            }
        }
    } 
}