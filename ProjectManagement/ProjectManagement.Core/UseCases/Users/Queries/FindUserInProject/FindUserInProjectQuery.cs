using System;
using MediatR;
using ProjectManagement.Core.UseCases.Users.ViewsModels;

namespace ProjectManagement.Core.UseCases.Users.Queries.FindUserInProject
{
    public class FindUserInProjectQuery : IRequest<UserVm>
    {
        public string Term { get; }
        public int ProjectId { get; }

        public FindUserInProjectQuery(string term, int projectId)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("Term can not be empty");
            }
            Term = term;
            ProjectId = projectId;
        }
    }
}
