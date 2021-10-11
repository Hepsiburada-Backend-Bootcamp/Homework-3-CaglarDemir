using Microsoft.AspNetCore.Mvc.Testing;
using Pharmacy.Application.CompanyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyAPI.Test
{
    public class CompanyIntegration_Test : IClassFixture<PharmacyApiFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public CompanyIntegration_Test(PharmacyApiFactory factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task Post_Should_Return_Fail_With_Error_Response_When_Insert_CompanyName_Is_Empty()
        {
            //Arrange

            var client = _factory.CreateClient();

            var company = new CompanyDto { CompanyName = string.Empty };

            var content = ContentCreator(company);


            //Act

            var response = await client.PostAsync("api/v1/firmalar", content);

            var actualStatusCode = response.StatusCode;

            //Assert

            Assert.Equal(HttpStatusCode.BadRequest, actualStatusCode);
        }

        [Fact]
        public async Task Post_Return_Ok_When_CompanyNameIsNotEmpty()
        {
            //Arrange

            var client = _factory.CreateClient();
            var company = new CompanyDto { CompanyName = "IntegrationTest" };
            var content = ContentCreator(company);

            //Act

            var response = await client.PostAsync("api/v1/firmalar", content);
            var actualStatusCode = response.StatusCode;

            //Assert

            Assert.Equal(HttpStatusCode.OK, actualStatusCode);

        }

        [Fact]
        public async Task Delete_Return_NotFound_When_CompanyIsNotFound()
        {
            //Arrange

            var client = _factory.CreateClient();

            //Act
            var listResponse = await client.GetAsync("api/v1/firmalar");

            var listJson = await listResponse.Content.ReadAsStringAsync();
            var companyList = JsonSerializer.Deserialize<List<CompanyDto>>(listJson);

            var responseDelete = await client.DeleteAsync($"api/v1/firmalar/{companyList.Count + 1}");
            var actualStatusCode = responseDelete.StatusCode;

            //Assert

            Assert.Equal(HttpStatusCode.NotFound, actualStatusCode);

        }




        private StringContent ContentCreator(object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

    }
}
