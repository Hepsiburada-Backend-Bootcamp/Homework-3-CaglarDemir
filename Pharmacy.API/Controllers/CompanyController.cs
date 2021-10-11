using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.Application.CompanyApp;

namespace Pharmacy.API.Controllers
{
    [Route("api/v1/firmalar")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _companyService.GetAll();
            if (result.Count > 0)
            {
                return Ok(result);

            }
            else
            {
                return NotFound("Firmalar bulunamadi!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _companyService.GetById(id);

            return Ok(result);

        }

        [HttpPost]
        public IActionResult Add([FromBody] CompanyDto companyDto)
        {
            _companyService.Add(companyDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _companyService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CompanyDto companyDto)
        {
            companyDto.CompanyId = id;
            _companyService.Update(companyDto);
            return Ok();
        }
    }
}
