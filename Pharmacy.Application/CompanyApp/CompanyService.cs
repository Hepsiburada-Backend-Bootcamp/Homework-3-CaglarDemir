using Mapster;
using Pharmacy.Domain.Repositories;
using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Pharmacy.Application.MedicineApp;

namespace Pharmacy.Application.CompanyApp
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task Add(CompanyDto companyDto)
        {
            return _companyRepository.Add(companyDto.Adapt<Company>());
        }

        public Task Delete(int id)
        {
            var result = _companyRepository.GetByCompanyId(id);

            return _companyRepository.Delete(result.Result);

        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var result = (await _companyRepository.GetAll()).OrderBy(x => x.CompanyId);
            return result.Adapt<List<CompanyDto>>();
        }

        public async Task<CompanyDto> GetById(int id)
        {
            var result = await _companyRepository.GetByCompanyId(id);
            if (result == null)
            {
                throw new ApplicationException("Firma bulunamadi!");
            }
            return result.Adapt<CompanyDto>();

        }

        public Task Update(CompanyDto companyDto)
        {
            var result = GetById(companyDto.CompanyId);

            return _companyRepository.Update(companyDto.Adapt<Company>());
        }

        //public async Task<List<CompanyDto>> Get(Expression<Func<CompanyDto, bool>> filter)
        //{
        //    var dtoFilter = filter.Adapt<Expression<Func<Company, bool>>>(); // bu dönüşüm çalışmadığı için filter kullanımı bir alt seviyede özelleştirilerek kullanılabilir. örn => GetById(id)
        //    var result = await _companyRepository.Get(dtoFilter);
        //    return result.Adapt<List<CompanyDto>>();
        //}
    }
}
