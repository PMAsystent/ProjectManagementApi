using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.UpdateBoss
{
    public class UpdateBossCommand : IRequest<UpdateBossCommandResponse>, IMapFrom<Boss>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateBossCommand, Boss>();
        }
    }
}
