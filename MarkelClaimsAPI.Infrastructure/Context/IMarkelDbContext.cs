using MarkelClaimsAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MarkelClaimsAPI.Infrastructure.Context
{
    public interface IMarkelDbContext
    {
        DbSet<Claim> Claims { get; }
        DbSet<Company> Companys { get; }
        DbSet<ClaimType> ClaimTypes { get; }
        Task<int> SaveChangesAysnc
            (CancellationToken cancellationToken = default(CancellationToken));
    }
}
