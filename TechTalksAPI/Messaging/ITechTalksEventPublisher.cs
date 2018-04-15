using TechTalksAPI.Models;

namespace TechTalksAPI.Messaging
{
    public interface ITechTalksEventPublisher
    {
        void SendMessage();
        void SendMessage(TechTalk talk);
    }
}