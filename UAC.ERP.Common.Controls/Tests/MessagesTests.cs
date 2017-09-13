using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAC.ERP.Common.Controls.Tests
{
    [ExcludeFromCodeCoverage]
	[TestClass]
    public class MessagesTests
    {
		[TestMethod]
		[TestCategory("Messages")]
		public void initializes_correctly()
        {
            var msgs = new Messages(objectID: 1, messageType: MessageType.AddCustomer);
            Assert.AreEqual(msgs.ObjectID, 1);
            Assert.AreEqual(msgs.MessageTypes, MessageType.AddCustomer);

            msgs = new Messages(objectIDInt: 1, messageType: MessageType.AddCustomer);
            Assert.AreEqual(msgs.ObjectIDInt, 1);
            Assert.AreEqual(msgs.MessageTypes, MessageType.AddCustomer);

            msgs = new Messages(Allobject: 0x0, messageType: MessageType.AddCustomer);
            Assert.AreEqual(msgs.AllObject, 0x0);
            Assert.AreEqual(msgs.MessageTypes, MessageType.AddCustomer);

            msgs = new Messages(MessageType.AddCustomer);
            Assert.AreEqual(msgs.MessageTypes, MessageType.AddCustomer);

            msgs = new Messages(str: "test", messageType: MessageType.AddCustomer);
            Assert.AreEqual(msgs.Str, "test");
            Assert.AreEqual(msgs.MessageTypes, MessageType.AddCustomer);

            msgs = new Messages(objectID: 1, objectID1: 2, messageType: MessageType.AddCustomer);
            Assert.AreEqual(msgs.ObjectID, 1);
            Assert.AreEqual(msgs.ObjectID1, 2);
            Assert.AreEqual(msgs.MessageTypes, MessageType.AddCustomer);
        }
    }
}
