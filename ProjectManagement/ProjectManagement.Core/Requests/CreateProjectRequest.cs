using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.Requests
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<string> AssignedEmails { get; set; }
    }
}