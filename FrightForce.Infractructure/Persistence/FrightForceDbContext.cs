using System.Data;
using FrightForce.Domain.Base;
using FrightForce.Domain.Documents;
using FrightForce.Domain.Identity;
using FrightForce.Infractructure.Persistence.Filters;
using FrightForce.Infractructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace FrightForce.Infractructure.Persistence;

public class FrightForceDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        int,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken
    >,
    ITransactionContextAwareDbContext, ICompanyScoped<int>
    {
        public const string DefaultSchema = "public";
        public int CompanyId { get; set; }
        private IDbContextTransaction _currentTransaction;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<FrightForceDbContext> _logger;

        public FrightForceDbContext(
            DbContextOptions options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
            ICurrentUserService currentUserService,
            ILogger<FrightForceDbContext> logger) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        #region DbSets
        //Document 
         public DbSet<DocumentType> DocumentTypes { get; set; } = null!;
         public DbSet<Domain.Documents.Document> Documents { get; set; } = null!;
         public DbSet<Docket> Dockets { get; set; } = null!;
         public DbSet<DocketDocument> DocketDocuments { get; set; } = null!;
  

        #endregion

        #region Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(DefaultSchema);

            // add soft delete query filter

            // modelBuilder.FilterSoftDeletedRecords();
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType) &&
                typeof(ICompanyScoped<int>).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddQueryFilters(this);
                }
                else
                {
                    entityType.FilterSoftDeletedRecords(this);
                }
            }

            // add entity type configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FrightForceDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            optionsBuilder.UseSnakeCaseNamingConvention();
        }
        #endregion

        #region DomainEvents
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            var domainEntities = ChangeTracker
                .Entries<IDomainEventAware>()
                .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents!)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            return domainEvents;
        }
        #endregion

        #region Transaction
        public async System.Threading.Tasks.Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is not null) return;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        }

        public async System.Threading.Tasks.Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
                await _currentTransaction?.CommitAsync(cancellationToken)!;
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public async System.Threading.Tasks.Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _currentTransaction?.RollbackAsync(cancellationToken)!;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public async System.Threading.Tasks.Task RestartTransactionAsync()
        {
            await CommitTransactionAsync();
            await BeginTransactionAsync();
        }

        #endregion
    }