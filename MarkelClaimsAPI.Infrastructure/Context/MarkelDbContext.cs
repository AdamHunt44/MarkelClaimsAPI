using MarkelClaimsAPI.Data.Models;
using MarkelClaimsAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkelClaimsAPI.Infrastructure.Context
{
    public class MarkelDbContext : DbContext, IMarkelDbContext
    {
        public MarkelDbContext(DbContextOptions<MarkelDbContext> options) : base(options) { }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }

        public Task<int> SaveChangesAysnc(CancellationToken cancellationToken = default) => base.SaveChangesAsync(cancellationToken);
    }
}
