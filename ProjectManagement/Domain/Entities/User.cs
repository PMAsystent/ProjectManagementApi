using Domain.Base;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string ApplicationUserId { get; set; }
    }
}