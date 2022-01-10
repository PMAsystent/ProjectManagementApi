using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using ProjectManagement.Core.Base.Utils;
using Xunit;

namespace ProjectManagement.Core.UnitTests.IProjectPercentageService
{
    public class GetProgressPercentageForTask
    {
        private readonly IProgressPercentageService _service = new ProgressPercentageService();

        [Fact]
        public void With0SubtaskAndStatusDone()
        {
            var correctValue = 100;

            var task = new Task()
            {
                TaskStatus = TaskStatus.Done.ToString()
            };

            var calculatedPercent = _service.GetProgressPercentageForTask(task);
            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void With3SubtaskNotDoneAndStatusDone()
        {
            var correctValue = 0;

            var task = new Task()
            {
                TaskStatus = TaskStatus.Done.ToString(),
                Subtasks = new List<Subtask>()
                {
                    new() { IsDone = false },
                    new() { IsDone = false },
                    new() { IsDone = false }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForTask(task);
            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void WithNotDoneSubtask()
        {
            var correctValue = 0;

            var task = new Task()
            {
                Subtasks = new List<Subtask>()
                {
                    new() { IsDone = false }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForTask(task);
            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void WithDoneSubtask()
        {
            var correctValue = 100;

            var task = new Task()
            {
                Subtasks = new List<Subtask>()
                {
                    new() { IsDone = true }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForTask(task);
            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void With1DoneSubtaskAnd1NotDone()
        {
            var correctValue = 50;

            var task = new Task()
            {
                Subtasks = new List<Subtask>()
                {
                    new() { IsDone = false },
                    new() { IsDone = true }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForTask(task);
            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void With1DoneSubtaskAnd4NotDone()
        {
            var correctValue = 20;

            var task = new Task()
            {
                Subtasks = new List<Subtask>()
                {
                    new() { IsDone = false },
                    new() { IsDone = false },
                    new() { IsDone = false },
                    new() { IsDone = false },
                    new() { IsDone = true }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForTask(task);
            calculatedPercent.Should().Be(correctValue);
        }

        [Fact]
        public void With6DoneSubtaskAnd4NotDone()
        {
            var correctValue = 60;

            var task = new Task()
            {
                Subtasks = new List<Subtask>()
                {
                    new() { IsDone = false },
                    new() { IsDone = false },
                    new() { IsDone = false },
                    new() { IsDone = false },
                    new() { IsDone = true },
                    new() { IsDone = true },
                    new() { IsDone = true },
                    new() { IsDone = true },
                    new() { IsDone = true },
                    new() { IsDone = true }
                }
            };

            var calculatedPercent = _service.GetProgressPercentageForTask(task);
            calculatedPercent.Should().Be(correctValue);
        }
    }
}