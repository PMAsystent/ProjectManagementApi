using System;
using System.Collections.Generic;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.Requests
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<CreateProjectAssignmentsDto> AssignedUsers { get; set; }
    }
}