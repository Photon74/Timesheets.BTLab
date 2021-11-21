using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Timesheets.DAL.Entitys;
using Timesheets.DAL.Interfaces;
using Timesheets.DTO;

namespace Timesheets.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly IAbsenceRepository _repository;
        private readonly IMapper _mapper;

        public AbsenceService(IAbsenceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(AbsenceDTO absenceDTO)
        {
            await _repository.CreateAsync(_mapper.Map<AbsenceEntity>(absenceDTO));
        }

        public async Task UpdateAsync(int id, AbsenceDTO absenceDTO)
        {
            await _repository.UpdateAsync(id, _mapper.Map<AbsenceEntity>(absenceDTO));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<AbsenceDTO> GetByIdAsync(int id)
        {
            var result = new AbsenceDTO();
            var res = await _repository.GetByIdAsync(id);

            if (res is not null)
            {
                result = _mapper.Map<AbsenceDTO>(res);
            }
            return result;
        }

        public async Task<List<AbsenceDTO>> GetAllAsync()
        {
            var absenceDTOList = new List<AbsenceDTO>();
            var list = await _repository.GetAllAsync();

            foreach (var absence in list)
            {
                absenceDTOList.Add(_mapper.Map<AbsenceDTO>(absence));
            }

            return absenceDTOList;
        }
    }
}
