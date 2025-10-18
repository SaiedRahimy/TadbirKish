using TadbirKish.DataReception.Domain.Common;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
