namespace TaxCalculator.Repositories.Companies
{
    // ....... Private limited company
    public class SelfEnterpriseCompany : ICompany
    {
        public string Name { get; set; }
        public double RegistrationNumber { get; set; }
        public double AnnualTurnover { get; set; }
        public double Tax { get; set; }

        
        
        public SelfEnterpriseCompany(string name, double registrationNumber, double annualTurnover)
        {
            Name = name;
            RegistrationNumber = registrationNumber;
            AnnualTurnover = annualTurnover;
            CalculateTax();

        }
        
        
         public void CalculateTax() => Tax = AnnualTurnover * 0.25;

    }
}