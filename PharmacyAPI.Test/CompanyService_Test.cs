using Moq;
using Pharmacy.Application.CompanyApp;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyAPI.Test
{
    public class CompanyService_Test
    {
        [Fact]
        public void GetAll_Return_TaskListCompanyDto()
        {
            // Arrange
            var companyRepositoryMock = new Mock<ICompanyRepository>();
            var expected = GetCompanies();
            companyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expected);

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act
            var actual = companyServiceMock.GetAll();

            //Assert
            Assert.NotNull(actual.Result);
            Assert.NotEmpty(actual.Result);
            Assert.Equal(expected.Count, actual.Result.Count);
        }


        [Fact]
        public void Add_CompanyDto()
        {
            // Arrange
            CompanyDto companyDto = new CompanyDto() { CompanyId = 1, CompanyName = "Company" };

            var companyRepositoryMock = new Mock<ICompanyRepository>();
            companyRepositoryMock.Setup(repo => repo.Add(It.IsAny<Company>()));

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act
            var actual = companyServiceMock.Add(companyDto);

            //Assert
            companyRepositoryMock.Verify(x => x.Add(It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public void Update_CompanyDto()
        {
            // Arrange
            CompanyDto companyDto = new CompanyDto() { CompanyId = 1, CompanyName = "Company" };

            var companyRepositoryMock = new Mock<ICompanyRepository>();
            companyRepositoryMock.Setup(repo => repo.Update(It.IsAny<Company>()));
            companyRepositoryMock.Setup(repo => repo.GetByCompanyId(It.IsAny<int>())).ReturnsAsync(GetCompanies()[0]);

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await companyServiceMock.Update(companyDto));
            companyRepositoryMock.Verify(x => x.GetByCompanyId(It.IsAny<int>()), Times.Once);
            companyRepositoryMock.Verify(x => x.Update(It.IsAny<Company>()), Times.Once);

        }
        [Fact]
        public void Update_CompanyDto_ThrowsException()
        {
            // Arrange
            CompanyDto companyDto = new CompanyDto() { CompanyId = 1, CompanyName = "Company" };

            var companyRepositoryMock = new Mock<ICompanyRepository>();
            companyRepositoryMock.Setup(repo => repo.Update(It.IsAny<Company>()));

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await companyServiceMock.Update(companyDto));
            companyRepositoryMock.Verify(x => x.GetByCompanyId(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void Delete_CompanyDto()
        {
            // Arrange

            var companyRepositoryMock = new Mock<ICompanyRepository>();
            companyRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Company>()));
            companyRepositoryMock.Setup(repo => repo.GetByCompanyId(It.IsAny<int>())).ReturnsAsync(GetCompanies()[0]);

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await companyServiceMock.Delete(1));
            companyRepositoryMock.Verify(x => x.GetByCompanyId(It.IsAny<int>()), Times.Once);
            companyRepositoryMock.Verify(x => x.Delete(It.IsAny<Company>()), Times.Once);

        }
        [Fact]
        public void Delete_CompanyDto_ThrowsException()
        {
            // Arrange

            var companyRepositoryMock = new Mock<ICompanyRepository>();
            companyRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Company>()));

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await companyServiceMock.Delete(1));
            companyRepositoryMock.Verify(x => x.GetByCompanyId(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void GetByCompanyId_Returns_CompanyDto()
        {
            // Arrange

            var companyRepositoryMock = new Mock<ICompanyRepository>();
            companyRepositoryMock.Setup(repo => repo.GetByCompanyId(It.IsAny<int>())).ReturnsAsync(GetCompanies()[0]);

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await companyServiceMock.GetById(1));
            companyRepositoryMock.Verify(x => x.GetByCompanyId(It.IsAny<int>()), Times.Once);
            Assert.NotNull(companyServiceMock.GetById(1));

        }

        [Fact]
        public void GetByCompanyId_Throws_ApplicationException()
        {
            // Arrange

            var companyRepositoryMock = new Mock<ICompanyRepository>();

            CompanyService companyServiceMock = new CompanyService(companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await companyServiceMock.GetById(1));
            companyRepositoryMock.Verify(x => x.GetByCompanyId(It.IsAny<int>()), Times.Once);
        }

        private List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            for (int i = 0; i < 10; i++)
            {
                Company company = new Company();
                company.CompanyId = i + 1;
                company.CompanyName = $"{i + 1}. Firma";

                companies.Add(company);
            }

            return companies;
        }
    }
}
