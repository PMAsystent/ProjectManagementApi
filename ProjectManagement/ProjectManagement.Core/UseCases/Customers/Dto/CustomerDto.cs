using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Customers.Dto
{
    public class CustomerDto : IMapFrom<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Customer, CustomerDto>();
        }
    }
}
