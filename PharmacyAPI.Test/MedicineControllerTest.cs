using Microsoft.AspNetCore.Mvc;
using Moq;
using Pharmacy.Application.MedicineApp;
using PharmacyAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyAPI.Test
{
    public class MedicineControllerTest
    {
        [Fact]
        public async void List_Return_Ok_MedicineDto()
        {
            //Arrange
            List<MedicineDto> list = GetMedicine();

            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new MedicineController(mockService.Object);

            //Act
            var result = await controller.List(null);

            //Assert
            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            List<MedicineDto> actualResult = returnValue.Value as List<MedicineDto>;
            Assert.NotEmpty(actualResult);
            Assert.Equal(list.Count, actualResult.Count);

        }

        [Fact]
        public async void List_CompanyNameX_Return_Ok_MedicineDtoFiltered()
        {
            //Arrange
            List<MedicineDto> list = GetMedicine();

            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new MedicineController(mockService.Object);

            //Act
            var result = await controller.List("CompanyName=\"X\"");

            //Assert
            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            List<MedicineDto> actualResult = returnValue.Value as List<MedicineDto>;
            Assert.NotEmpty(actualResult);
            Assert.Equal(list.Count, actualResult.Count);
            mockService.Verify(x => x.GetAll(), Times.Once);

        }
        [Fact]
        public async void List_CompanyNameY_Return_NotFound()
        {
            //Arrange
            List<MedicineDto> list = GetMedicine();

            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new MedicineController(mockService.Object);

            //Act
            var result = await controller.List("CompanyName=\"Y\""); //Olmayan bir firma adi

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);

            var returnValue = result as NotFoundObjectResult;
            string actualResult = returnValue.Value as string;
            Assert.Equal("İstenilen kriterlere uygun ilaç bulunamadı!", actualResult);
            mockService.Verify(x => x.GetAll(), Times.Once);

        }

        [Fact]
        public void List_CompanyY_Return_NotFound()
        {
            //Arrange
            List<MedicineDto> list = GetMedicine();

            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new MedicineController(mockService.Object);

            //Act

            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await controller.List("Company=\"X\""));
            mockService.Verify(x => x.GetAll(), Times.Once);


        }


        private List<MedicineDto> GetMedicine()
        {
            List<MedicineDto> medicines = new();
            for (int i = 1; i <= 10; i++)
            {
                MedicineDto medicine = new()
                {
                    CompanyName = "X",
                    MedicineId = i,
                    Name = $"{i}. Ilac ",
                    UnitPrice = i,
                    UnitsInStock = i,
                    ExpirationDate = DateTime.Now.AddDays(i),
                    Details = ""
                };

                medicines.Add(medicine);
            }
            return medicines;
        }
    }
}
