using TechTalksAPI.Models;

namespace TechTalksAPI.Messaging
{
    public interface ITechTalksEventPublisher
    {
        void SendMessage(TechTalk talk);
    }
}