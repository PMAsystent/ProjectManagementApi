using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Enums;
using FluentAssertions;
using ProjectManagement.Core.UseCases.ProjectAssignments.Commands.AddUserToProject;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.ProjectAssignments
{
    [Collection("Sequential")]
    public class AddUserToProject :IntegrationTest
    {
        public AddUserToProject(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }
        
        [Fact]
        public async Task AddUserToProjectSuccessful()
        {
            var request = new AddUserToProjectCommand()
            {
                UserId = Utilities.User2.Id,
                ProjectId = Utilities.Project1.Id,
                MemberType = ProjectMemberType.Manager.ToString(),
                ProjectRole = ProjectRole.SuperMember.ToString()
            };
            
            var response = await Client.PostAsync("/api/ProjectAssignments", Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AddUserToProjectWhenUnauthorized()
        {
            var request = new AddUserToProjectCommand()
            {
                UserId = Utilities.User1.Id,
                ProjectId = Utilities.Project2.Id,
                MemberType = ProjectMemberType.Manager.ToString(),
                ProjectRole = ProjectRole.SuperMember.ToString()
            };
            
            var response = await Client.PostAsync("/api/ProjectAssignments", Utilities.GetRequestContent(request));
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        // [Fact]
        // public async Task AddUserToProjectWhichUserAlreadyBelong()
        // {
        //     throw new NotImplementedException();
        // }
        
        // [Fact]
        // public async Task AddUserToProjectWithIncorrectMemberType()
        // {
        //     throw new NotImplementedException();
        // }
        
        // [Fact]
        // public async Task AddUserToProjectWithIncorrectProjectRole()
        // {
        //     throw new NotImplementedException();
        // }
        
        // [Fact]
        // public async Task AddUserToNotActive()
        // {
        //     throw new NotImplementedException();
        // }
    }
}