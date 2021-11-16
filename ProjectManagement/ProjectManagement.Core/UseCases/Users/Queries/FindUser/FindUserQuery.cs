using System;
using MediatR;
using ProjectManagement.Core.UseCases.Users.ViewsModels;

namespace ProjectManagement.Core.UseCases.Users.Queries.FindUser
{
    public class FindUserQuery : IRequest<UserVm>
    {
        public string Term { get; }

        public FindUserQuery(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("Term can not be empty");
            }
            Term = term;
        }
    }
}
