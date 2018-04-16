using TechTalksModel;

namespace TechTalksAPI.Messaging
{
    public interface ITechTalksEventPublisher
    {
        void SendMessage(TechTalk talk);
    }
}