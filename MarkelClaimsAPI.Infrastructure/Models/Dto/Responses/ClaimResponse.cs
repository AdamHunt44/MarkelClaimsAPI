namespace MarkelClaimsAPI.Infrastructure.Models.Dto.Responses
{
    public class ClaimResponse
    {
        public int Id { get; set; }
        public required string UCR { get; set; }
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public required string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int ClaimAgeInDays { get; set; }
    }
}
