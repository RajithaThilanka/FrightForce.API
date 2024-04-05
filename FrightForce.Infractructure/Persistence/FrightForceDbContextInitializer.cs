using Microsoft.EntityFrameworkCore;

namespace FrightForce.Infractructure.Persistence;

public class FrightForceDbContextInitializer
{
    private readonly FrightForceDbContext _context;

    public FrightForceDbContextInitializer(FrightForceDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                    
                await _context.Database.MigrateAsync();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();

        await _context.SaveChangesAsync();


    }
}