using Mapster;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pharmacy.API.Controllers;
using Pharmacy.Application.CompanyApp;
using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyAPI.Test
{
    public class CompanyController_Test
    {
        [Fact]
        public async void GetAll_Return_Ok_Companies()
        {
            //Arrange
            List<CompanyDto> list = GetCompanies();

            var mockService = new Mock<ICompanyService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new CompanyController(mockService.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            List<CompanyDto> actualResult = returnValue.Value as List<CompanyDto>;
            Assert.NotEmpty(actualResult);
            Assert.Equal(list.Count, actualResult.Count);
        }

        [Fact]
        public async void GetCompanyList_Return_NotFound()
        {
            //Arrange
            List<CompanyDto> list = new();

            var mockService = new Mock<ICompanyService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new CompanyController(mockService.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);

            var returnValue = result as NotFoundObjectResult;
            string actualResult = returnValue.Value as string;
            Assert.Equal("Firmalar bulunamadi!", actualResult);
        }

        [Fact]
        public async void Get_Return_Ok_CompanyDto()
        {
            //Arrange
            List<CompanyDto> list = GetCompanies();

            var mockService = new Mock<ICompanyService>();
            mockService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync((int x) => list[x]);

            var controller = new CompanyController(mockService.Object);

            //Act
            var result = await controller.Get(3);

            //Assert
            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            CompanyDto actualResult = returnValue.Value as CompanyDto;
            Assert.NotNull(actualResult);
            Assert.Equal(list[3], actualResult);

        }

        [Fact]
        public void Get_ThrowsApplicationException()
        {
            //Arrange
            var mockService = new Mock<ICompanyService>();
            mockService.Setup(service => service.GetById(It.IsAny<int>())).ThrowsAsync(new ApplicationException("Firma bulunamadi!"));

            var controller = new CompanyController(mockService.Object);

            //Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await controller.Get(3));

            mockService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void Add_Return_Ok()
        {
            //Arrange

            var mockService = new Mock<ICompanyService>();

            mockService.Setup(service => service.Add(It.IsAny<CompanyDto>()));

            var controller = new CompanyController(mockService.Object);

            //Act
            var result = controller.Add(GetCompanies()[0]);

            //Assert
            Assert.IsType<OkResult>(result);
            mockService.Verify(x => x.Add(It.IsAny<CompanyDto>()), Times.Once);

        }


        [Fact]
        public void Delete_Return_Ok()
        {
            //Arrange
            var mockService = new Mock<ICompanyService>();

            mockService.Setup(service => service.Delete(It.IsAny<int>()));

            var controller = new CompanyController(mockService.Object);
            //Act
            controller.Delete(4);

            //Assert
            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void Delete_ThrowsApplicationException()
        {
            //Arrange
            var mockService = new Mock<ICompanyService>();

            mockService.Setup(service => service.Delete(It.IsAny<int>())).Throws(new ApplicationException("Firma bulunamadi!"));

            var controller = new CompanyController(mockService.Object);
            //Act
            
            //Assert

            Assert.Throws<ApplicationException>(() => controller.Delete(4));

            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void Update_Return_Ok()
        {
            //Arrange
            var mockService = new Mock<ICompanyService>();

            mockService.Setup(service => service.Update(It.IsAny<CompanyDto>()));

            var controller = new CompanyController(mockService.Object);
            //Act
            controller.Update(5,GetCompanies()[5]);

            //Assert
            mockService.Verify(x => x.Update(It.IsAny<CompanyDto>()), Times.Once);

        }

        [Fact]
        public void Update_ThrowsApplicationException()
        {
            //Arrange
            var mockService = new Mock<ICompanyService>();

            mockService.Setup(service => service.Update(It.IsAny<CompanyDto>())).Throws(new ApplicationException("Firma bulunamadi!"));

            var controller = new CompanyController(mockService.Object);
            //Act

            //Assert

            Assert.Throws<ApplicationException>(() => controller.Update(5, GetCompanies()[5]));

            mockService.Verify(x => x.Update(It.IsAny<CompanyDto>()), Times.Once);

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
