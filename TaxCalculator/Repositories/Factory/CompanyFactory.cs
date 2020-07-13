using TaxCalculator.Repositories.Companies;

namespace TaxCalculator.Repositories.Factory
{
    public class CompanyFactory : ICompanyFactory
    {
        public ICompany Create(string name, double registrationNumber, double annualTurnover, string address)
        {
            if (string.IsNullOrEmpty(address))
            {

                return new SelfEnterpriseCompany(name , registrationNumber , annualTurnover);
            }

            return new SASCompany(name , registrationNumber , annualTurnover , address);
        }
    }
}