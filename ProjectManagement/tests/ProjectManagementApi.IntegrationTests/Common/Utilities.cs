using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Identity;
using Infrastructure.Persistance.DatabaseContext;
using Newtonsoft.Json;
using Task = Domain.Entities.Task;
using TaskStatus = Domain.Enums.TaskStatus;

namespace ProjectManagementApi.IntegrationTests.Common
{
    public static class Utilities
    {
        private static ApplicationDbContext Context { get; set; }
        public static User User1 { get; private set; } = null!;
        public static Project Project1 { get; private set; } = null!;
        public static Step P1Step1 { get; private set; }= null!;
        public static Task P1S1Task1 { get; private set; } = null!;
        public static Task P1S1Task2 { get; private set; } = null!;
        public static Task P1S1Task3 { get; private set; } = null!;


        public static void InitializeDbForTests(ApplicationDbContext context)
        {
            Context = context;
            ResetDb();
            SeedForTests();
        }

        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        private static void ResetDb()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        private static void SeedForTests()
        {
            //TODO: implement seed
            SeedUsers();
            SeedProjects();
        }

        private static void SeedUsers()
        {
            var authUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "integration@tests.pl",
                UserName = "integration.tests"
            };
            Context.Add(authUser);
            Context.SaveChanges();

            var user = new User()
            {
                Email = authUser.Email,
                UserName = authUser.UserName,
                ApplicationUserId = authUser.Id
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            User1 = user;
        }

        private static void SeedProjects()
        {
            var task1 = new Task()
            {
                Name = "p1s1 task1",
                Priority = TaskPriority.High.ToString(),
                TaskStatus = TaskStatus.ToDo.ToString(),
                DueDate = DateTime.UtcNow.AddDays(2),
                Subtasks = new List<Subtask>()
                {
                    new()
                    {
                        Name = "p1s1t1 subtask1",
                        IsDone = true
                    },
                    new()
                    {
                        Name = "p1s1t1 subtask2",
                        IsDone = false
                    },
                }
            };

            var task2 = new Task()
            {
                Name = "p1s1 task2",
                Priority = TaskPriority.High.ToString(),
                TaskStatus = TaskStatus.Done.ToString(),
                DueDate = DateTime.UtcNow.AddDays(2),
                Subtasks = new List<Subtask>()
                {
                    new()
                    {
                        Name = "p1s1t2 subtask1",
                        IsDone = true
                    },
                    new()
                    {
                        Name = "p1s1t2 subtask2",
                        IsDone = false
                    },
                }
            };

            var task3 = new Task()
            {
                Name = "p1s1 task3",
                Priority = TaskPriority.High.ToString(),
                TaskStatus = TaskStatus.Done.ToString(),
                DueDate = DateTime.UtcNow.AddDays(2),
            };

            var step1 = new Step()
            {
                Name = "p1 step1",
                Tasks = new List<Task>()
                {
                    task1,
                    task2,
                    task3,
                    new()
                    {
                        Name = "p1s1 task4",
                        Priority = TaskPriority.High.ToString(),
                        TaskStatus = TaskStatus.Done.ToString(),
                        DueDate = DateTime.UtcNow.AddDays(2),
                    }
                }
            };

            var project = new Project()
            {
                Name = "project1",
                DueDate = DateTime.UtcNow.AddDays(14),
                IsActive = true,
                Assigns = new List<ProjectAssignment>()
                {
                    new()
                    {
                        MemberType = ProjectMemberType.Manager.ToString(),
                        ProjectRole = ProjectRole.SuperMember.ToString(),
                        UserId = User1.Id
                    }
                },
                Steps = new List<Step>() { step1 }
            };

            Context.Projects.Add(project);
            Context.SaveChanges();

            Project1 = project;
            P1Step1 = step1;
            P1S1Task1 = task1;
            P1S1Task2 = task2;
            P1S1Task3 = task3;
        }

    }
}