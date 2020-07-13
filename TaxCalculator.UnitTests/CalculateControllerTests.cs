using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using TaxCalculator.Controllers;
using TaxCalculator.Entities;
using TaxCalculator.Repositories.Companies;
using TaxCalculator.Repositories.Factory;
using Xunit;

namespace TaxCalculator.UnitTests
{
    public class CalculateControllerTests
    {
        private readonly Mock<ICompanyFactory> _moqCalculatorFactory;
        private readonly CalculateController _controller;

        public CalculateControllerTests()
        {
            _moqCalculatorFactory = new Mock<ICompanyFactory>();
            _moqCalculatorFactory.SetupAllProperties();
            _controller = new CalculateController(_moqCalculatorFactory.Object);
        }


        [Fact]
        public void PostCompanyDetails_InvalidModelPassed_ReturnsBadRequest()
        {
            // Arrange 
            CompanyModel companyModel = new CompanyModel
            {
                RegistrationNumber = 123456,
                AnnualTurnover = 1000
            };

            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _controller.PostCompanyDetails(companyModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostCompanyDetails_SASModelProvided_ReturnsOKWith33PercentOfAnnualTurnover()
        {
            // Arrange 
            CompanyModel companyModel = new CompanyModel
            {
                Name = "DForce",
                RegistrationNumber = 123456,
                AnnualTurnover = 1000,
                Address = "Paris - France" // SAS Companies have Address 
            };

            _moqCalculatorFactory.Setup(x => x.Create(companyModel.Name, companyModel.RegistrationNumber,
                    companyModel.AnnualTurnover, companyModel.Address))
                .Returns(new SASCompany(companyModel.Name, companyModel.RegistrationNumber, companyModel.AnnualTurnover,
                    companyModel.Address));


            // Act
            var result = _controller.PostCompanyDetails(companyModel);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<SASCompany>(okResult.Value);
            Assert.Equal(companyModel.AnnualTurnover * 0.33, model.Tax);
        }

        [Fact]
        public void PostCompanyDetails_SelfEnterpriseModelProvided_ReturnsOKWith25PercentOfAnnualTurnover()
        {
            // Arrange 
            CompanyModel companyModel = new CompanyModel
            {
                Name = "DForce",
                RegistrationNumber = 123456,
                AnnualTurnover = 1000,
            };

            _moqCalculatorFactory.Setup(x =>
                    x.Create(companyModel.Name, companyModel.RegistrationNumber, companyModel.AnnualTurnover, null))
                .Returns(new SelfEnterpriseCompany(companyModel.Name, companyModel.RegistrationNumber,
                    companyModel.AnnualTurnover));


            // Act
            var result = _controller.PostCompanyDetails(companyModel);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<SelfEnterpriseCompany>(okResult.Value);
            Assert.Equal(companyModel.AnnualTurnover * 0.25, model.Tax);
        }
    }
}