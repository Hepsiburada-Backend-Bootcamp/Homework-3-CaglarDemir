using Microsoft.AspNetCore.Mvc;
using Moq;
using Pharmacy.API.Controllers;
using Pharmacy.Application.CompanyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyAPI.Test
{
    public class CompanyControllerTest
    {
        [Fact]
        public async void GetCompanyList_Return_Companies()
        {
            var mockService = new Mock<ICompanyService>();
            var list = GetCompanies();

            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new CompanyController(mockService.Object);

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            List<CompanyDto> actualResult = returnValue.Value as List<CompanyDto>;
            Assert.NotEmpty(actualResult);
            Assert.Equal(list.Count, actualResult.Count);
        }

        private List<CompanyDto> GetCompanies()
        {
            List<CompanyDto> companies = new List<CompanyDto>();
            for (int i = 0; i < 10; i++)
            {
                CompanyDto company = new();
                company.CompanyId = i + 1;
                company.CompanyName = $"{i + 1}. Firma";

                companies.Add(company);
            }

            return companies;
        }
    }
}
