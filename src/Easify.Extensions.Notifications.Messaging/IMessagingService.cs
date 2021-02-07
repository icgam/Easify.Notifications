using System.Threading.Tasks;

namespace Easify.Extensions.Notifications.Messaging
{
    public interface IMessagingService
    {
        Task SendAsync(Message message);
    }
}