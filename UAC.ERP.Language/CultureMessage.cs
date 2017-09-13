namespace UAC.ERP.Language
{
    public enum MessageType { LangChanged, CultureChanged, DBChanged };

    public class Message
    {
        public MessageType type { get; private set; }

        public Message(MessageType mt)
        {
            this.type = mt;
        }
    }
}