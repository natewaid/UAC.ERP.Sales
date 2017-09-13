using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UAC.ERP.Sales.Models.Tests;
using UAC.ERP.Sales.PO.ViewModels;
using UAC.ERP.Sales.Business.Operation;
using UAC.ERP.Sales.Models;
using System.Diagnostics.CodeAnalysis;

namespace UAC.ERP.Sales.PO.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BaseViewModelTests
    {
        private Mock<IStatusHelper> statusHelper { get; set; }

        private Mock<ITermHelper> termHelper { get; set; }

        private PoHeadViewModel model { get; set; }

        public BaseViewModelTests()
        {
            statusHelper = new Mock<IStatusHelper>();
            statusHelper.Setup(k => k.GetAllStatuses()).Returns(new List<Status>()
            {
                new Status { Id = 1, Description = "test1" },
                new Status { Id = 2, Description = "test2" }
            });

            termHelper = new Mock<ITermHelper>();
            termHelper.Setup(k => k.GetAllTerms()).Returns(new List<Term>()
            {
                new Term { Id = 1, Code = "test1", TermType = 1 },
                new Term { Id = 2, Code = "test2", TermType = 2 }
            });

            termHelper.Setup(k => k.GetAllTermTypes()).Returns(new List<TermType>()
            {
                new TermType { Id = 1, Description = "Payment" },
                new TermType { Id = 2, Description = "Delivery" }
            });

            // po head model is used since it inherits from base
            // cannot use base because it is abstract
            model = new PoHeadViewModel();
            model.StatusHelper = statusHelper.Object;
            model.TermHelper = termHelper.Object;
        }

        [TestMethod]
        [TestCategory("Base View Model")]
        public void status_items_returnS_correctly()
        {
            Assert.AreEqual(2, model.StatusItems.Count);
            Assert.IsTrue(model.StatusItems.Any(s => s.Key.Equals(1)));
            Assert.IsTrue(model.StatusItems.Any(s => s.Key.Equals(2)));
        }

        [TestMethod]
        [TestCategory("Base View Model")]
        public void payment_term_items_returns_correctly()
        {
            Assert.AreEqual(1, model.PaymentTermItems.Count);
            Assert.IsTrue(model.PaymentTermItems.Any(s => s.Key.Equals(1)));
        }

        [TestMethod]
        [TestCategory("Base View Model")]
        public void delivery_term_items_returns_correctly()
        {
            Assert.AreEqual(1, model.DeliveryTermItems.Count);
            Assert.IsTrue(model.DeliveryTermItems.Any(s => s.Key.Equals(2)));
        }
    }
}
