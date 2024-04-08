using FrightForce.Application.Base;
using FrightForce.Domain.Documents;

namespace FrightForce.Application.Features.Document.Docket.Commands;

public class CreateDocketCommand : ICommand<Domain.Documents.Docket>
{
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public class CreateDocketCommandHandler : ICommandHandler<CreateDocketCommand, Domain.Documents.Docket>
    {
        private readonly IDocumentService _documentService;
        public CreateDocketCommandHandler(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        public async Task<Result<Domain.Documents.Docket>> Handle(CreateDocketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var docket = await _documentService.CreateDocketAsync(request.CompanyId, request.Name);
                return Result<Domain.Documents.Docket>.Ok(docket);
            }
            catch (Exception ex)
            {
                return Result<Domain.Documents.Docket>.Fail(ex.Message);
            }
        }
    }
}
