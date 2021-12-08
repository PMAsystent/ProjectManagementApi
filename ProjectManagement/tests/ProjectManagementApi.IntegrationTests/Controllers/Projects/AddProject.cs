using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using ProjectManagement.Core.Requests;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementApi.IntegrationTests.Controllers.Projects
{
    [Collection("Sequential")]
    public class AddProject : IntegrationTest
    {
        public AddProject(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory,
            output)
        {
        }

        [Fact]
        public async Task AddProjectWithoutAssignmentsSuccessful()
        {
            var projectName = "Test project";

            var project = new CreateProjectRequest()
            {
                Name = projectName,
                Description = "This is project created for integration test",
                DueDate = DateTime.UtcNow.AddDays(7),
                AssignedUsers = new List<CreateProjectAssignmentsDto>()
            };

            var response = await Client.PostAsync("/api/MyProjects", Utilities.GetRequestContent(project));
            response.EnsureSuccessStatusCode();

            var responseContent = await Utilities.GetResponseContent<DetailedProjectDto>(response);
            responseContent.Id.Should().BeGreaterThan(0);
            responseContent.Name.Should().Be(projectName);
            responseContent.ProjectSteps.Count.Should().Be(1);
            responseContent.ProjectAssignedUsers.Count.Should().BeGreaterOrEqualTo(1);
            responseContent.CurrentUserInfoInProject.MemberType.Should().Be(ProjectMemberType.Manager.ToString());
            responseContent.CurrentUserInfoInProject.ProjectRole.Should().Be(ProjectRole.SuperMember.ToString());
        }

        // [Fact]
        // public async Task AddProjectWithAssignmentsSuccessful()
        // {
        //     throw new NotImplementedException();
        // }

        [Fact]
        public async Task AddProjectWithEmptyName()
        {
            var project = new CreateProjectRequest()
            {
                Name = "",
                Description = "This is project created for integration test",
                DueDate = DateTime.UtcNow.AddDays(7),
                AssignedUsers = new List<CreateProjectAssignmentsDto>()
            };

            var response = await Client.PostAsync("/api/MyProjects", Utilities.GetRequestContent(project));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddProjectWithDueDateEarlierThanNow()
        {
            var project = new CreateProjectRequest()
            {
                Name = "xyz",
                Description = "This is project created for integration test",
                DueDate = DateTime.UtcNow.AddDays(-1),
                AssignedUsers = new List<CreateProjectAssignmentsDto>()
            };

            var response = await Client.PostAsync("/api/MyProjects", Utilities.GetRequestContent(project));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        // [Fact]
        // public async Task AddProjectWithAssignedUsersWithIncorrectContent()
        // {
        //     //TODO: Check if project is created, if creator is assgined etc.
        //     throw new NotImplementedException();
        // }
    }
}