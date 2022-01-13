using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Enums;
using ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask;
using ProjectManagement.Core.UseCases.Tasks.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Tasks
{
    [Collection("Sequential")]
    public class AddTask : IntegrationTest
    {
        public AddTask(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task AddTaskWithAssignmentsSuccessful()
        {
            var request = new CreateTaskCommand()
            {
                Name = "new task",
                Description = "test description",
                DueDate = DateTime.UtcNow,
                Priority = TaskPriority.High.ToString(),
                AssignedUsers = new List<CreateTaskAssignmentDto>()
                {
                    new() { UserId = Utilities.User3.Id }
                },
                StepId = Utilities.P1Step1.Id
            };

            var response = await Client.PostAsync("api/Tasks", Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();
        }
        
        // [Fact]
        // public async Task AddTaskWithAssignmentsNotFromProject()
        // {
        //     throw new NotImplementedException();
        // }
        
        // [Fact]
        // public async Task AddTaskWithoutAssignmentsSuccessful()
        // {
        //     throw new NotImplementedException();
        // }
        
        // validation tests...
    }
}