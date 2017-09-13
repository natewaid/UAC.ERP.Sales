using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace UAC.ERP.Sales.Models.Tests
{
    [ExcludeFromCodeCoverage]
    public class MockDbContextHelper
    {
        public Mock<DbSet<POHead>> MockPoHeads { get; set; }

        public Mock<DbSet<POLine>> MockPoLines { get; set; }

        public Mock<DbSet<Status>> MockStatuses { get; set; }

        public Mock<DbSet<Term>> MockTerms { get; set; }

        public Mock<DbSet<TermType>> MockTermTypes { get; set; }

        public Mock<Production2016Entities> MockDbContext()
        {
            MockPoHeads = new Mock<DbSet<POHead>>();
            MockPoLines = new Mock<DbSet<POLine>>();
            MockStatuses = new Mock<DbSet<Status>>();
            MockTerms = new Mock<DbSet<Term>>();
            MockTermTypes = new Mock<DbSet<TermType>>();

            var context = new Mock<Production2016Entities>();

            context.Setup(k => k.POHeads).Returns(MockPoHeads.Object);
            context.Setup(k => k.POLines).Returns(MockPoLines.Object);
            context.Setup(k => k.Statuses).Returns(MockStatuses.Object);
            context.Setup(k => k.Terms).Returns(MockTerms.Object);
            context.Setup(k => k.TermTypes).Returns(MockTermTypes.Object);            

            return context;
        }
    }
}
