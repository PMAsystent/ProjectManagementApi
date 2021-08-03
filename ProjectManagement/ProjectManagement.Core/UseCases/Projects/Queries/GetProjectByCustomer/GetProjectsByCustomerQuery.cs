using MediatR;
using ProjectManagement.Core.UseCases.Projects.ViewModels;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectByCustomer
{
    public class GetProjectsByCustomerQuery : IRequest<ProjectVm>
    {
        public int CustomerId { get; set; }
    }
}
