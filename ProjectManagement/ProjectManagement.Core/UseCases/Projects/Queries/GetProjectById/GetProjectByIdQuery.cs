﻿using MediatR;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<DetailedProjectDto>
    {
        public int ProjectId { get; set; }
    }
}
