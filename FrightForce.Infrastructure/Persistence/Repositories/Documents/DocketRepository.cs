using FrightForce.Domain.Documents;
using FrightForce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FrightForce.Infractructure.Persistence.Repositories.Documents;

public class DocketRepository: BaseEfCoreRepository<Docket, int>,IDocketRepository
{
    
    private new readonly FrightForceDbContext _context;

    public DocketRepository(FrightForceDbContext context):base(context)
    {
        _context = context;
    }
    public async Task<Docket?> FetchDocketWithDocumentsAsync(int docketId)
    {
        return await _context.Dockets
            .Include(d => d.Documents.OrderByDescending(d => d.CreatedAt))
            .Include(d => d.Documents.OrderByDescending(d => d.CreatedAt))
            .FirstOrDefaultAsync(d => d.Id == docketId);

      
    }
}