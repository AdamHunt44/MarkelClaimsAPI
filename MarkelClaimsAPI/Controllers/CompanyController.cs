using MarkelClaimsAPI.Infrastructure.Context;
using MarkelClaimsAPI.Services.Interfaces;
using MarkelClaimsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarkelClaimsAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IMarkelDbContext _dbContext;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, IMarkelDbContext DbContext)
        {
            _logger = logger;
            _companyService = companyService;
            _dbContext = DbContext;
        }


        [HttpGet("CheckActiveClaims")]
        public ActionResult CheckActiveClaims(int companyId)
        {
            try
            {
                var claimResponse = _companyService.CheckActiveClaims(companyId);
                return Ok(claimResponse);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "No Claims found.");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "$An error occurred: {ex.message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

    }
}
