using Mapster;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.MedicineApp
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ICompanyRepository _companyRepository;

        private readonly TypeAdapterSetter _configToMedicineDto = TypeAdapterConfig<(Company, Medicine), MedicineDto>.NewConfig()
                .Map(d => d, s => s.Item2)
                .Map(d => d.CompanyName, s => s.Item1.CompanyName);

        private readonly TypeAdapterSetter _configToMedicine = TypeAdapterConfig<(Company, MedicineDto), Medicine>.NewConfig()
                .Map(d => d, s => s.Item2)
                .Map(d => d.CompanyId, s => s.Item1.CompanyId);


        public MedicineService(IMedicineRepository medicineRepository, ICompanyRepository companyRepository)
        {
            _medicineRepository = medicineRepository;
            _companyRepository = companyRepository;
        }

        public Task Add(MedicineDto medicineDto)
        {
            var result = _companyRepository.Get(c => c.CompanyName == medicineDto.CompanyName);

            if (result.Result == null)
            {
                throw new ApplicationException("Firma bulunamadi!");
            }

            Company company = result.Result.ElementAt(0);

            Medicine data = (company, medicineDto).Adapt<Medicine>(_configToMedicine.Config);

            return _medicineRepository.Add(data);
        }

        public Task Delete(int id)
        {
            var result = _medicineRepository.GetByMedicineId(id);
            return _medicineRepository.Delete(result.Result);
        }

        public async Task<List<MedicineDto>> GetAll()
        {
            List<MedicineDto> data = new List<MedicineDto>();
            var result = await _medicineRepository.GetWithCompany(a => true);
            foreach (var item in result)
            {
                data.Add((item.Company, item).Adapt<MedicineDto>(_configToMedicineDto.Config));
            }

            return data;
        }

        public async Task<MedicineDto> GetById(int id)
        {
            var medicine = await _medicineRepository.GetByMedicineId(id);
            if (medicine == null)
            {
                throw new ApplicationException("İlaç bulunamadi!");
            }

            var result = (medicine.Company, medicine).Adapt<MedicineDto>(_configToMedicineDto.Config);
            return result;

        }

        public Task Update(MedicineDto medicineDto)
        {
            var result = _companyRepository.Get(c => c.CompanyName == medicineDto.CompanyName);
            var medicine = _medicineRepository.GetByMedicineId(medicineDto.MedicineId);
            
            if (result.Result == null)
            {
                throw new ApplicationException("Firma bulunamadi!");
            }

            Company company = result.Result.ElementAt(0);

            Medicine data = (company, medicineDto).Adapt<Medicine>(_configToMedicine.Config);

            return _medicineRepository.Update(data);
        }
    }
}
