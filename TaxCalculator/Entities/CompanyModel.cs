using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Entities
{
    public class CompanyModel
    {
        [Required] 
        public string Name { get; set; }

        [Required] 
        public double RegistrationNumber { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter Positive doubleNumber")]
        public double AnnualTurnover { get; set; }

        public string Address { get; set; }

    }
}