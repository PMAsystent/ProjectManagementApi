using System;
using MediatR;
using ProjectManagement.Core.UseCases.Projects.ViewModels;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetMyProjectsList
{
    public class GetMyProjectsQuery : IRequest<MyProjectsListVm>
    {
        public string CurrentUserGuid { get; }

        public GetMyProjectsQuery(string currentUserGuid)
        {
            CurrentUserGuid = currentUserGuid ?? throw new Exception("Current user GUID is null.");
        }
    }
}
