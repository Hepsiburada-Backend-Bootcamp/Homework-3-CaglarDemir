using Moq;
using Pharmacy.Application.MedicineApp;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyAPI.Test
{
    public class MedicineService_Test
    {
        Mock<IMedicineRepository> medicineRepositoryMock = new Mock<IMedicineRepository>();
        Mock<ICompanyRepository> companyRepositoryMock = new Mock<ICompanyRepository>();

        MedicineDto medicineDto1 = new MedicineDto()
        {
            MedicineId = 1,
            CompanyName = "Y", // companyId = 2
            ExpirationDate = DateTime.Now.AddDays(1),
            Name = "1. ilac",
            UnitPrice = 1,
            UnitsInStock = 1,
            Details = ""
        };

        MedicineDto medicineDto2 = new MedicineDto()
        {
            MedicineId = 2,
            CompanyName = "X", // companyId = 1
            ExpirationDate = DateTime.Now.AddDays(2),
            Name = "2. ilac",
            UnitPrice = 2,
            UnitsInStock = 2,
            Details = ""
        };

        [Fact]
        public void GetAll_Return_TaskListMedicineDto()
        {
            // Arrange
            var expected = GetMedicines();
            medicineRepositoryMock.Setup(repo => repo.GetWithCompany(a => true)).ReturnsAsync(expected);

            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act
            var actual = medicineServiceMock.GetAll();

            //Assert
            Assert.NotNull(actual.Result);
            Assert.NotEmpty(actual.Result);
            Assert.Equal(expected.Count, actual.Result.Count);
        }



        [Fact]
        public void Add_MedicineDto()
        {
            // Arrange
            List<Company> companies = new List<Company>();
            companies.Add(new Company()
            {
                CompanyId = 2,
                CompanyName = "Y"
            });

            medicineRepositoryMock.Setup(repo => repo.Add(It.IsAny<Medicine>()));
            companyRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(companies);

            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act
            medicineServiceMock.Add(medicineDto1);

            //Assert
            companyRepositoryMock.Verify(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
            medicineRepositoryMock.Verify(x => x.Add(It.IsAny<Medicine>()), Times.Once);
        }

        [Fact]
        public void Add_ThrowsException_CompanyNotFound()
        {
            //Arrange
            medicineRepositoryMock.Setup(repo => repo.Add(It.IsAny<Medicine>()));
            companyRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Company, bool>>>()));

            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act
            

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await medicineServiceMock.Add(medicineDto1));

            companyRepositoryMock.Verify(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
            medicineRepositoryMock.Verify(x => x.Add(It.IsAny<Medicine>()), Times.Never);
        }

        [Fact]
        public void Update_MedicineDto()
        {
            List<Company> companies = new List<Company>();
            companies.Add(new Company()
            {
                CompanyId = 2,
                CompanyName = "Y"
            });

            // Arrange
            medicineRepositoryMock.Setup(repo => repo.Update(It.IsAny<Medicine>()));
            medicineRepositoryMock.Setup(repo => repo.GetByMedicineId(It.IsAny<int>())).ReturnsAsync(GetMedicines()[0]);
            companyRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(companies);


            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act
            medicineServiceMock.Update(medicineDto1);

            //Assert
            medicineRepositoryMock.Verify(x => x.GetByMedicineId(It.IsAny<int>()), Times.Once);
            medicineRepositoryMock.Verify(x => x.Update(It.IsAny<Medicine>()), Times.Once);
            companyRepositoryMock.Verify(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);

        }
        [Fact]
        public void Update_MedicineDto_ThrowsException_CompanyNotFound()
        {
            // Arrange

            medicineRepositoryMock.Setup(repo => repo.Update(It.IsAny<Medicine>()));
            medicineRepositoryMock.Setup(repo => repo.GetByMedicineId(It.IsAny<int>())).ReturnsAsync(GetMedicines()[0]);

            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await medicineServiceMock.Update(medicineDto1));
            medicineRepositoryMock.Verify(x => x.GetByMedicineId(It.IsAny<int>()), Times.Once);
            medicineRepositoryMock.Verify(x => x.Update(It.IsAny<Medicine>()), Times.Never);

        }

        [Fact]
        public void Delete_MedicineDto()
        {
            // Arrange
            medicineRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Medicine>()));
            medicineRepositoryMock.Setup(repo => repo.GetByMedicineId(It.IsAny<int>())).ReturnsAsync(GetMedicines()[0]);

            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act
            medicineServiceMock.Delete(1);

            //Assert

            medicineRepositoryMock.Verify(x => x.GetByMedicineId(It.IsAny<int>()), Times.Once);
            medicineRepositoryMock.Verify(x => x.Delete(It.IsAny<Medicine>()), Times.Once);

        }


        [Fact]
        public void GetByMedicineId_Returns_MedicineDto()
        {
            // Arrange

            medicineRepositoryMock.Setup(repo => repo.GetByMedicineId(It.IsAny<int>())).ReturnsAsync(GetMedicines()[0]);

            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await medicineServiceMock.GetById(1));
            medicineRepositoryMock.Verify(x => x.GetByMedicineId(It.IsAny<int>()), Times.Once);
            Assert.NotNull(medicineServiceMock.GetById(1));

        }

        [Fact]
        public void GetByMedicineId_Throws_ApplicationException()
        {
            // Arrange

            MedicineService medicineServiceMock = new MedicineService(medicineRepositoryMock.Object, companyRepositoryMock.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await medicineServiceMock.GetById(1));
            medicineRepositoryMock.Verify(x => x.GetByMedicineId(It.IsAny<int>()), Times.Once);
        }
        private List<Medicine> GetMedicines()
        {
            List<Medicine> medicines = new List<Medicine>();

            for (int i = 1; i <= 10; i++)
            {
                Medicine medicine = new Medicine();
                medicine.MedicineId = i;
                medicine.CompanyId = (i % 2) + 1;
                medicine.Name = $"{i}. ilac";
                medicine.ExpirationDate = DateTime.Now.AddDays(i);
                medicine.UnitPrice = i;
                medicine.UnitsInStock = i;
                medicine.Details = "";
                Company company = new Company();

                company.CompanyId = (i % 2) + 1;

                if ((i % 2) == 0)
                {
                    company.CompanyName = "X";
                }
                else
                {
                    company.CompanyName = "Y";
                }
                medicine.Company = company;

                medicines.Add(medicine);
            }

            return medicines;
        }
    }
}
