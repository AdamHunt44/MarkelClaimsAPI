using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkelClaimsAPI.Infrastructure.Context;
using MarkelClaimsAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarkelClaimsAPI.Services
{
    public class CompanyService : ICompanyService
    {

        private readonly ILogger<CompanyService> _logger;
        private readonly IMarkelDbContext _dbContext;

        public CompanyService(ILogger<CompanyService> logger, IMarkelDbContext DbContext)
        {
            _logger = logger;
            _dbContext = DbContext;
        }


        public bool CheckActiveClaims(int companyId)
        {
            var result = _dbContext.Claims.Where(
                x => x.CompanyId == companyId
                && x.Closed == false
            ).ToList();
            return result.Any();
        }
    }
}
