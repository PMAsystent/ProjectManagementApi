using Domain.Base;

namespace Domain.Events
{
    public class UserRegisteredEvent : DomainEvent
    {
        public string ApplicationUserId { get; }

        public UserRegisteredEvent(string applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }
    }
}