namespace TaxCalculator.Repositories.Companies
{
    public interface ICompany
    {
        string Name { get; set; }
        double RegistrationNumber { get; set; }
        double AnnualTurnover { get; set; }
        double Tax { get; set; }

        void CalculateTax();
        
        // ....... I did not specified any Constant variable for the tax rate because we might have progressive tax in future
    }
}