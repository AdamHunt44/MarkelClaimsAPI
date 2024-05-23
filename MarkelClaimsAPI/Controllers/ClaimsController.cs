using MarkelClaimsAPI.Data.Models;
using MarkelClaimsAPI.Infrastructure.Context;
using MarkelClaimsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarkelClaimsAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ClaimsController(ILogger<ClaimsController> logger, IClaimService claimService, IMarkelDbContext DbContext) : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger = logger;
        private readonly IClaimService _claimService = claimService;
        private readonly IMarkelDbContext _dbContext = DbContext;

        [HttpGet("GetClaimById")]
        public ActionResult GetClaimById(string claimRef)
        {
            try
            {
                var result = _claimService.GetClaimDetails(claimRef);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "No Claims found.");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet("GetClaimByCompanyId")]
        public ActionResult GetClaimByCompanyId(int companyId)
        {
            try
            {
                var result = _claimService.GetClaimsByCompany(companyId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "No Claims found.");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPut("EditClaim")]
        public ActionResult EditClaim(Claim claim)
        {
            try
            {
                _claimService.EditClaim(claim);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "No Claims found.");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
