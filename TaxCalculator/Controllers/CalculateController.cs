using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Entities;
using TaxCalculator.Repositories.Companies;
using TaxCalculator.Repositories.Factory;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {

        private readonly ICompanyFactory _companyFactory;

        public CalculateController(ICompanyFactory companyFactory)
        {

            _companyFactory = companyFactory;
        }
        
        
        
        [HttpPost]
        public  IActionResult PostCompanyDetails(CompanyModel companyModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(companyModel);
            }

            ICompany company = _companyFactory.Create(companyModel.Name, companyModel.RegistrationNumber,
                companyModel.AnnualTurnover, companyModel.Address);
            
            
            return Ok(company);
        }
        
    }
}