using Domain.Base;

namespace Domain.Events
{
    public class UserRegisteredEvent : DomainEvent
    {
        public string ApplicationUserId { get; }
        public string ApplicationUserName { get; }
        public string ApplicationUserEmail { get; }

        public UserRegisteredEvent(string applicationUserId, string applicationUserName, string applicationUserEmail)
        {
            ApplicationUserId = applicationUserId;
            ApplicationUserName = applicationUserName;
            ApplicationUserEmail = applicationUserEmail;
        }
    }
}