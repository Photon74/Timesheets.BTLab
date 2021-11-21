using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.DTO;

namespace Timesheets.Services
{
    public interface IAbsenceService
    {
        Task CreateAsync(AbsenceDTO absenceDTO);
        Task DeleteAsync(int id);
        Task<List<AbsenceDTO>> GetAllAsync();
        Task<AbsenceDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, AbsenceDTO absenceDTO);
    }
}