using MarkelClaimsAPI.Services.Interfaces;
using MarkelClaimsAPI.Infrastructure.Context;
using Microsoft.Extensions.Logging;
using MarkelClaimsAPI.Data.Models;
using AutoMapper;
using MarkelClaimsAPI.Infrastructure.Models.Dto.Responses;

namespace MarkelClaimsAPI.Services
{
    public class ClaimService : IClaimService
    {
        private readonly ILogger<ClaimService> _logger;
        private readonly IMarkelDbContext _dbContext;
        private readonly IMapper _mapper;

        public ClaimService(ILogger<ClaimService> logger, IMarkelDbContext DbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = DbContext;
            _mapper = mapper;
        }

        public List<Claim> GetClaimsByCompany(int companyId)
        {
            try
            {
                var claimDetails = _dbContext.Claims.Where(x => x.CompanyId == companyId).ToList();

                return claimDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving claim details.");
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public ClaimResponse GetClaimDetails(string claimRef)
        {
            try
            {
                var result = _dbContext.Claims.FirstOrDefault(x => x.UCR == claimRef);

                if (result == null)
                {
                    throw new ArgumentException($"No claim found with reference: {claimRef}");
                }
                var Dto = _mapper.Map<ClaimResponse>(result);
                Dto.ClaimAgeInDays = GetClaimAgeInDays(result.ClaimDate);
                return Dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving claim details.");
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public void EditClaim(Claim claim)
        {
            try
            {
                var result = _dbContext.Claims.SingleOrDefault(x => x.UCR == claim.UCR);

                if (result == null)
                {
                    throw new ArgumentException($"No claim found with reference: {claim.UCR}");
                }

                // Replace with automapper
                result.AssuredName = claim.AssuredName;
                result.ClaimDate = claim.ClaimDate;
                result.LossDate = claim.LossDate;
                result.Closed = claim.Closed;
                result.CompanyId = claim.CompanyId;

                _dbContext.SaveChangesAysnc();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating claim details.");
                throw new ApplicationException("An error occurred while updating claim details.", ex);
            }
        }

        private int GetClaimAgeInDays(DateTime claimDate)
        {
            if (claimDate < DateTime.UtcNow)
            {
                var dateDiff = DateTime.UtcNow - claimDate;
                return dateDiff.Days;
            }
            else
            {
                throw new ApplicationException("ClaimDate is in the future.");
            }
        }
    }
}
