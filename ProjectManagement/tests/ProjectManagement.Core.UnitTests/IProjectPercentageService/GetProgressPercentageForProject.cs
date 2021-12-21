using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using ProjectManagement.Core.Base.Utils;
using Xunit;

namespace ProjectManagement.Core.UnitTests.IProjectPercentageService
{
    public class GetProgressPercentageForProject
    {
        private readonly IProgressPercentageService _service = new ProgressPercentageService();
        
        [Fact]
        public void With1Step2Tasks()
        {
            const int correctValue = 50;

            var project = new Project()
            {
                Steps = new List<Step>()
                {
                    new()
                    {
                        Tasks = new List<Task>()
                        {
                            new() { TaskStatus = TaskStatus.Done.ToString() },
                            new() { TaskStatus = TaskStatus.InProgress.ToString() }
                        }
                    },
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForProject(project.Steps.ToList());
            
            calculatedPercent.Should().Be(correctValue);
        }
        
        [Fact]
        public void With2Step4Tasks()
        {
            const int correctValue = 50;

            var project = new Project()
            {
                Steps = new List<Step>()
                {
                    new()
                    {
                        Tasks = new List<Task>()
                        {
                            new() { TaskStatus = TaskStatus.Done.ToString() },
                            new() { TaskStatus = TaskStatus.InProgress.ToString() }
                        }
                    },
                    new()
                    {
                        Tasks = new List<Task>()
                        {
                            new() { TaskStatus = TaskStatus.Done.ToString() },
                            new() { TaskStatus = TaskStatus.InProgress.ToString() }
                        }
                    },
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForProject(project.Steps.ToList());
            
            calculatedPercent.Should().Be(correctValue);
        }
        
        [Fact]
        public void WithoutTasks()
        {
            const int correctValue = 0;

            var project = new Project()
            {
                Steps = new List<Step>()
                {
                    new()
                    {
                        Tasks = new List<Task>()
                        {
                        }
                    }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForProject(project.Steps.ToList());
            
            calculatedPercent.Should().Be(correctValue);
        }

    }
}