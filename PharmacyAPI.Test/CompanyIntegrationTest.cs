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
    public class CompanyIntegrationTest : IClassFixture<PharmacyApiFactory>
    {
        //private readonly WebApplicationFactory<TestStartup> _factory;

        //public CompanyIntegrationTest(PharmacyApiFactory factory)
        //{
        //    _factory = factory;
        //}


        //[Fact]
        //public async Task Post_Should_Return_Fail_With_Error_Response_When_Insert_CompanyName_Is_Empty()
        //{
        //    //Arrange

        //    var company = new CompanyDto { CompanyName = string.Empty };

        //    var json = JsonSerializer.Serialize(company);

        //    //var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress=new System.Uri( "http://localhost:44381" ) });

        //    var client = _factory.CreateClient();

        //    var content = new StringContent(json, Encoding.UTF8, "application/json");


        //    //Act

        //    var response = await client.PostAsync("api/v1/firmalar", content);

        //    var actualStatusCode = response.StatusCode;

        //    //Assert

        //    Assert.Equal(HttpStatusCode.BadRequest, actualStatusCode);
        //}

        //[Fact]
        //public async Task Post_Should_Return_Success_With_CompanyId_Response()
        //{
        //    //Arrange

        //    var company = new CompanyDto { CompanyName = "Test Company" };

        //    var json = JsonSerializer.Serialize(company);

        //    var client = _factory.CreateClient();

        //    var content = new StringContent(json, Encoding.UTF8, "application/json");


        //    //Act

        //    var response = await client.PostAsync("api/v1/firmalar", content); //Post response

        //    //Assert

        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //    var resultData = await response.Content.ReadAsStringAsync();

        //    var okResult = JsonSerializer.Deserialize<dynamic>(resultData);

        //    Assert.True(okResult.GetProperty("status").GetBoolean());

        //    var responseGetAll = await client.GetAsync("api/v1/firmalar");

        //    responseGetAll.EnsureSuccessStatusCode();

        //    var companyListData = await responseGetAll.Content.ReadAsStringAsync();

        //    var companyListDataJsonElement = JsonSerializer.Deserialize<JsonElement>(companyListData);

        //    var companyList = JsonSerializer.Deserialize<List<CompanyDto>>(companyListDataJsonElement.GetProperty("data").GetRawText());

        //    Assert.NotNull(companyList);
        //    Assert.NotEmpty(companyList);

        //    var lastCompany = companyList.Last();

        //    Assert.Equal(company.CompanyName, lastCompany.CompanyName);
        //}
    }
}
