using Domain.Base;

namespace Domain.Events
{
    public class UserEmailChangedEvent : DomainEvent
    {
        public string ApplicationUserEmail { get; }
        public string ApplicationUserNewEmail { get; }

        public UserEmailChangedEvent(string applicationUserEmail, string applicationUserNewEmail)
        {
            ApplicationUserEmail = applicationUserEmail;
            ApplicationUserNewEmail = applicationUserNewEmail;
        }
    }
}