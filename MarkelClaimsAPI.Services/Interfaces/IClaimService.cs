using MarkelClaimsAPI.Data.Models;
using MarkelClaimsAPI.Infrastructure.Models.Dto.Responses;

namespace MarkelClaimsAPI.Services.Interfaces
{
    public interface IClaimService
    {
        public List<Claim> GetClaimsByCompany(int companyId);
        public ClaimResponse GetClaimDetails(string claimRef);
        public void EditClaim(Claim claim);
    }
}