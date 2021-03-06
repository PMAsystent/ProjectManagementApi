using Domain.Base;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Base.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
