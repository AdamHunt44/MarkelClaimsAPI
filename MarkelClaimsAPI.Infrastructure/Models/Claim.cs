using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace MarkelClaimsAPI.Data.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public required string UCR { get; set; }
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public required string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
    }
}
