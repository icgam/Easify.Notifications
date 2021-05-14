using System.Threading.Tasks;

namespace Easify.Notifications.Messaging
{
    public interface IMessagingService
    {
        Task SendAsync(Message message);
    }
}