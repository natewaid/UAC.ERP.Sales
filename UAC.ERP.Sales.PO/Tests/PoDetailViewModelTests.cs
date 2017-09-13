using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UAC.ERP.Common.Controls;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.Business.Operation;
using UAC.ERP.Sales.PO.ViewModels;

namespace UAC.ERP.Sales.PO.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]    
    public class PoDetailViewModelTests
    {
        private Mock<IPOHelper> poHelper { get; set; }

        public PoDetailViewModelTests()
        {
            poHelper = new Mock<IPOHelper>();
            poHelper.Setup(k => k.GetPo(It.IsAny<int>())).Returns(new PoHeadModel { POHeadID = 1 });
            poHelper.Setup(k => k.GetPoLine(It.IsAny<int>())).Returns(new PoLineModel { POHeadPOHeadID = 1 });
        }

        [TestMethod]
        [TestCategory("Po Detail View Model")]
        public void initializes_correctly()
        {
            var model = new PoDetailViewModel();
            model.GetAppBarButtons();

            Assert.IsTrue(model.MessageCommands.Any());
            Assert.IsTrue(model.AppBarButtons.Any());
        }

        [TestMethod]
        [TestCategory("Po Detail View Model")]
        public void msg_receive_calls_correct_method()
        {
            var model = new PoDetailViewModel();
            model.PoHelper = poHelper.Object;

            model.OnMsgReceive(new Messages(1, MessageType.NewPoLine));

            Assert.AreEqual(1, model.PoDetailSelected.POHeadPOHeadID);

            model.OnMsgReceive(new Messages(1, MessageType.EditPoLine));

            Assert.AreEqual(1, model.PurchaseOrder.POHeadID);
        }
    }
}
