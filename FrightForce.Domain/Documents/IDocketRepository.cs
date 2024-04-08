using FrightForce.Domain.Base;

namespace FrightForce.Domain.Documents;

public interface IDocketRepository: IRepository<Docket, int>
{
    Task<Docket?> FetchDocketWithDocumentsAsync(int docketId);
}