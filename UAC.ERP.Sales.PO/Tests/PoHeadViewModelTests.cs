using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UAC.ERP.Common.Controls;
using UAC.ERP.Sales.Business.Operation;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.PO.ViewModels;

namespace UAC.ERP.Sales.PO.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PoHeadViewModelTests
    {
        private Mock<IPOHelper> poHelper { get; set; }

        public PoHeadViewModelTests()
        {
            poHelper = new Mock<IPOHelper>();
            poHelper.Setup(k => k.GetPo(It.IsAny<int>())).Returns(new PoHeadModel { POHeadID = 1 });
        }

        [TestMethod]
        [TestCategory("Po Head View Model")]
        public void initializes_correctly()
        {
            var model = new PoHeadViewModel();
            model.GetAppBarButtons();

            Assert.IsTrue(model.MessageCommands.Any());
            Assert.IsTrue(model.AppBarButtons.Any());
        }

        [TestMethod]
        [TestCategory("Po Head View Model")]
        public void msg_receive_calls_correct_method()
        {
            var model = new PoHeadViewModel();
            model.PoHelper = poHelper.Object;

            model.OnMsgReceive(new Messages(1, MessageType.ViewPoHead));

            Assert.AreEqual(1, model.PoHead.POHeadID);

            model.OnMsgReceive(new Messages(1, MessageType.AddNewPoHead));

            Assert.IsNotNull(model.PoHead);
            Assert.IsTrue(model.AppBarButtons.Any());
        }
    }
}
