using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkelClaimsAPI.Infrastructure
{
    public class MarkelDbContext(
        DbContextOptions<MarkelDbContext> options) : DbContext(options)
    {
    }
}
