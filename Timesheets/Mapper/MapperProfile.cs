using AutoMapper;
using Timesheets.DAL.Entitys;
using Timesheets.DTO;

namespace Timesheets.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AbsenceDTO, AbsenceEntity>();
            CreateMap<AbsenceEntity, AbsenceDTO>();
        }
    }
}
