namespace TaxCalculator.Repositories.Companies
{
    // ....... stock company
    public class SASCompany : ICompany
    {
        public string Name { get; set; }
        public double RegistrationNumber { get; set; }
        public double AnnualTurnover { get; set; }
        public double Tax { get; set; }
        public string Address { get; set; }

        public SASCompany(string name, double registrationNumber, double annualTurnover, string address)
        {
            Name = name;
            RegistrationNumber = registrationNumber;
            AnnualTurnover = annualTurnover;
            Address = address;
            CalculateTax();
        }
        
        
        public void CalculateTax() => Tax = AnnualTurnover * 0.33;

    }
}