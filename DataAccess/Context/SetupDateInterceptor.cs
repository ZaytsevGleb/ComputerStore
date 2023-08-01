using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccess.Context;

public class SetupDateInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity)
            .ToList();

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;
            var auditable = entry.Entity as BaseEntity;

            if (entry.State == EntityState.Modified)
            {
                auditable!.ModifiedDate = now;
            }
            if (entry.State == EntityState.Added)
            {
                auditable!.CreatedDate = now;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
