using TaxCalculator.Repositories.Companies;

namespace TaxCalculator.Repositories.Factory
{
    public interface ICompanyFactory
    {
        ICompany Create(string name, double registrationNumber, double annualTurnover, string address);
    }
}