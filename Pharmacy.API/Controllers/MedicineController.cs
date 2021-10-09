using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.MedicineApp;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Helper;
using Pharmacy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controllers
{
    [ApiController]
    [Route("api/v1/ilaclar")]

    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] MedicineDto medicineDto)
        {
            _medicineService.Add(medicineDto);
            return Ok(new { status = true, errors = "" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _medicineService.GetById(id);
            return Ok(new { status = true, data = result, errors = "" });

        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MedicineDto medicineDto)
        {
            medicineDto.MedicineId = id;
            _medicineService.Update(medicineDto);
            return Ok(new { status = true, errors = "" });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _medicineService.Delete(id);
            return Ok(new { status = true, errors = "" });

        }

        [HttpGet]
        [Route("[action]")] //  api/v1/ilaclar/List?list=CompanyName="Y"
        public async Task<IActionResult> List([FromQuery] string list)
        {
            var result = await _medicineService.GetAll();
            var data = result.AsQueryable();
            data = data.FilterSource(list);
            var dataList = data.ToList();
            if (dataList.Count == 0)
            {
                return NotFound("İstenilen kriterlere uygun ilaç bulunamadı!");
            }
            return Ok(data.ToList());
        }

        [HttpGet]
        [Route("[action]")] //  api/v1/ilaclar/Sort?sort=name
        public async Task<IActionResult> Sort([FromQuery] string sort)
        {
            var result = await _medicineService.GetAll();
            var data = result.AsQueryable();
            data = data.SortSource(sort);
            return Ok(new { status = true, data = data, errors = "" });

        }

        //Sort ve List actionlarında var olmayan bir field yollandığında oluşan hatayı nasıl handle edebileceğimi bulamadığım için 500 döndürüyor.
    }
}
