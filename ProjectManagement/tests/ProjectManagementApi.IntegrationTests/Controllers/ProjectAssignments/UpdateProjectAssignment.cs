using System;
using Domain.Entities;
using Domain.Enums;
using ProjectManagement.Core.UseCases.ProjectAssignments.Commands.UpdateProjectAssignment;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementApi.IntegrationTests.Controllers.ProjectAssignments
{
    [Collection("Sequential")]
    public class UpdateProjectAssignment :IntegrationTest
    {
        public UpdateProjectAssignment(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task UpdateProjectAssignmentSuccessful()
        {
            var request = new UpdateProjectAssignmentCommand()
            {
                UserId = Utilities.User3.Id,
                ProjectId = Utilities.Project1.Id,
                MemberType = ProjectMemberType.Tester.ToString(),
                ProjectRole = ProjectRole.SuperMember.ToString()
            };
            
            var response = await Client.PutAsync("/api/ProjectAssignments", Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();
        }
        
        // [Fact]
        // public async Task AddUserToProjectWhenUnauthorized()
        // {
        //     throw new NotImplementedException();
        // }
        
        // [Fact]
        // public async Task UpdateProjectAssignmentWithIncorrectMemberType()
        // {
        //     throw new NotImplementedException();
        // }
        
        // [Fact]
        // public async Task UpdateProjectAssignmentWithIncorrectProjectRole()
        // {
        //     throw new NotImplementedException();
        // }
    }
}