using AutoMapper;
using MarkelClaimsAPI.Data.Models;
using MarkelClaimsAPI.Infrastructure.Context;
using MarkelClaimsAPI.Infrastructure.Models.Dto.Responses;
using MarkelClaimsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarkelClaimsAPI.Tests
{
    public class ClaimServiceTests
    {
        private Mock<IMarkelDbContext> _dbContext;
        private ILogger<ClaimService> _logger;
        private IMapper _mapper;
        private Mock<DbSet<Claim>> mockSet;

        [SetUp]
        public void Setup()
        {
            _dbContext = new Mock<IMarkelDbContext>();
            mockSet = new Mock<DbSet<Claim>>();
            _logger = new Mock<ILogger<ClaimService>>().Object;

            var config = new MapperConfiguration(x => {
                x.CreateMap<Claim, ClaimResponse>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void GetClaim_Successs()
        {
            var claimService = new ClaimService(_logger, _dbContext.Object, _mapper);
            var ValidClaims = new List<Claim>
            {
                new Claim
                {
                    Id = 1,
                    AssuredName = "Adam",
                    UCR = "TEST-999",
                    ClaimDate = DateTime.UtcNow.AddDays(-10),
                    Closed = false,
                    CompanyId = 1,
                    IncurredLoss = 100,
                    LossDate = DateTime.Now
                }
            }.AsQueryable();
            mockSet = new Mock<DbSet<Claim>>();
            mockSet.As<IQueryable<Claim>>().Setup(m => m.Provider).Returns(ValidClaims.Provider);
            mockSet.As<IQueryable<Claim>>().Setup(m => m.Expression).Returns(ValidClaims.Expression);
            mockSet.As<IQueryable<Claim>>().Setup(m => m.ElementType).Returns(ValidClaims.ElementType);
            mockSet.As<IQueryable<Claim>>().Setup(m => m.GetEnumerator()).Returns(() => ValidClaims.GetEnumerator());

            _dbContext.Setup(x => x.Claims).Returns(mockSet.Object);
            Assert.That(() => claimService.GetClaimDetails("TEST-999").ClaimAgeInDays == 10);
        }
    }
}