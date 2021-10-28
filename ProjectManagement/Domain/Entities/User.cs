using System;
using Domain.Base;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ApplicationUserId { get; set; }
    }
}