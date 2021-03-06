using MediatR;
using System;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
    }
}
