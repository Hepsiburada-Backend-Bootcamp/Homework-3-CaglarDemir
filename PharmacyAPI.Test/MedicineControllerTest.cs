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

        [Fact]
        public void Add_Return_Ok()
        {
            //Arrange
            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.Add(It.IsAny<MedicineDto>()));

            var controller = new MedicineController(mockService.Object);

            //Act

            var result =  controller.Add(GetMedicine()[0]);

            //Assert
            Assert.IsType<OkResult>(result);
            mockService.Verify(x => x.Add(It.IsAny<MedicineDto>()), Times.Once);
        }

        [Fact]
        public async void Get_Return_Ok_MedicineDtoById()
        {
            //Arrange
            List<MedicineDto> list = GetMedicine();

            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync((int x)=>list[x]);

            var controller = new MedicineController(mockService.Object);

            //Act
            var result = await controller.Get(4);
            //Assert
            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            MedicineDto actualResult = returnValue.Value as MedicineDto;
            Assert.NotNull(actualResult);
            Assert.Equal(list[4], actualResult);

        }

        [Fact]
        public void Delete_Return_Ok()
        {
            //Arrange
            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.Delete(It.IsAny<int>()));

            var controller = new MedicineController(mockService.Object);
            //Act
            controller.Delete(4);

            //Assert
            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void Update_Return_Ok()
        {
            //Arrange
            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.Update(It.IsAny<MedicineDto>()));

            var controller = new MedicineController(mockService.Object);
            //Act
            controller.Update(5, GetMedicine()[5]);

            //Assert
            mockService.Verify(x => x.Update(It.IsAny<MedicineDto>()), Times.Once);

        }

        [Fact]
        public async void Sort_Return_Ok_MedicineDtoList()
        {
            //Arrange
            List<MedicineDto> list = GetMedicine();

            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new MedicineController(mockService.Object);
            //Act
            var result = await controller.Sort(null);

            //Assert
            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            List<MedicineDto> actualResult = returnValue.Value as List<MedicineDto>;

            Assert.NotNull(actualResult);
            Assert.Equal(list, actualResult);
            mockService.Verify(x => x.GetAll(), Times.Once);

        }

        [Fact]
        public async void Sort_Name_Return_Ok_MedicineDtoList()
        {
            //Arrange
            List<MedicineDto> list = GetMedicine();

            var mockService = new Mock<IMedicineService>();
            mockService.Setup(service => service.GetAll()).ReturnsAsync(list);

            var controller = new MedicineController(mockService.Object);
            //Act
            var result = await controller.Sort("name");

            //Assert
            Assert.IsType<OkObjectResult>(result);

            var returnValue = result as OkObjectResult;
            List<MedicineDto> actualResult = returnValue.Value as List<MedicineDto>;

            Assert.NotNull(actualResult);
            Assert.Equal(list.Count, actualResult.Count);
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
