using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAC.ERP.Sales.Models.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        [TestCategory("Model Extensions")]
        public void converts_to_observable_kvp()
        {
            var data = new List<Term>()
            {
                new Term { Id = 1, Code = "test1", Plant = 1 },
                new Term { Id = 2, Code = "test2", Plant = 2 },
                new Term { Id = 3, Code = "test3", Plant = 3 }
            };

            var result = data.ToObservableCollectionOfKvp<Term, int, string>(nameof(Term.Id), nameof(Term.Code));

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(r => r.Key.Equals(1)));
            Assert.IsTrue(result.Any(r => r.Key.Equals(2)));
            Assert.IsTrue(result.Any(r => r.Key.Equals(3)));
            Assert.IsTrue(result.Any(r => r.Value.Equals("test1")));
            Assert.IsTrue(result.Any(r => r.Value.Equals("test2")));
            Assert.IsTrue(result.Any(r => r.Value.Equals("test3")));
        }

        [TestMethod]
        [TestCategory("Model Extensions")]
        public void converts_to_observable()
        {
            var data = new List<int>() { 1, 2, 3 };

            var result = data.ToObservableCollection();

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(2));
            Assert.IsTrue(result.Contains(3));            
        }

        [TestMethod]
        [TestCategory("Model Extensions")]
        public void copies_object()
        {
            var from = new Term() { Id = 1, Code = "test1" };
            var to = new Term();

            from.CopyTo(to);

            Assert.AreEqual(1, to.Id);
            Assert.AreEqual("test1", to.Code);
        }
    }
}
