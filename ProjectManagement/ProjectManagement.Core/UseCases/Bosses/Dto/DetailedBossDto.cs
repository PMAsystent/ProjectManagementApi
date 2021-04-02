using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Bosses.Dto
{
    public class DetailedBossDto : IMapFrom<Boss>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Boss, DetailedBossDto>();
        }
    }
}
