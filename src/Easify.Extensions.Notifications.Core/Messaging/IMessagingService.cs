using System.Threading.Tasks;

namespace Easify.Extensions.Notifications.Core.Messaging
{
    public interface IMessagingService
    {
        Task SendAsync(Message message);
    }
}