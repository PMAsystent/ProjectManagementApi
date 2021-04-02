using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.CreateBoss
{
    public class CreateBossCommand : IRequest<CreateBossCommandResponse>, IMapFrom<Boss>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateBossCommand, Boss>();
        }
    }
}
