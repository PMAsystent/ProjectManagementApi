using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using ProjectManagement.Core.Base.Utils;
using Xunit;

namespace ProjectManagement.Core.UnitTests.IProjectPercentageService
{
    public class GetProgressPercentageForStep
    {
        private readonly IProgressPercentageService _service = new ProgressPercentageService();


        [Fact]
        public void With5Task()
        {
            var correctValue = 40;
            var step = new Step()
            {
                Tasks = new List<Task>()
                {
                    new() { TaskStatus = TaskStatus.Done.ToString() },
                    new() { TaskStatus = TaskStatus.Done.ToString() },
                    new() { TaskStatus = TaskStatus.InProgress.ToString() },
                    new() { TaskStatus = TaskStatus.InProgress.ToString() },
                    new() { TaskStatus = TaskStatus.InProgress.ToString() }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForStep(step.Tasks.ToList());

            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void With2Task()
        {
            var correctValue = 50;
            var step = new Step()
            {
                Tasks = new List<Task>()
                {
                    new() { TaskStatus = TaskStatus.Done.ToString() },
                    new() { TaskStatus = TaskStatus.InProgress.ToString() }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForStep(step.Tasks.ToList());

            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void WithoutTasks()
        {
            var correctValue = 0;
            var step = new Step()
            {
                Tasks = new List<Task>()
                {
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForStep(step.Tasks.ToList());

            calculatedPercent.Should().Be(correctValue);
        }
    }
}