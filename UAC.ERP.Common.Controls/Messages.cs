using System;

namespace UAC.ERP.Common.Controls
{
    public class Messages
    {
        public MessageType MessageTypes { get; private set; }

        public long ObjectID { get; private set; }

        public int ObjectIDInt { get; private set; }

        public long ObjectID1 { get; private set; }

        public string Str { get; private set; }

        public object AllObject { get; private set; }

        public Messages(long objectID, MessageType messageType)
        {
            ObjectID = objectID;
            MessageTypes = messageType;
        }

        public Messages(int objectIDInt, MessageType messageType)
        {
            ObjectIDInt = objectIDInt;
            MessageTypes = messageType;
        }

        public Messages(object Allobject, MessageType messageType)
        {
            AllObject = Allobject;
            MessageTypes = messageType;
        }

        public Messages(MessageType messageType)
        {
            MessageTypes = messageType;

        }

        public Messages(string str, MessageType messageType)
        {
            MessageTypes = messageType;
            Str = str;

        }

        public Messages(long objectID, long objectID1, MessageType messageType)
        {
            MessageTypes = messageType;
            ObjectID = objectID;
            ObjectID1 = objectID1;
        }
    }
}
