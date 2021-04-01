using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Bosses.Dto
{
    public class BossDto : IMapFrom<Boss>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Boss, BossDto>();
        }
    }
}
