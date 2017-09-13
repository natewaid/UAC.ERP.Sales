using System.Collections.Generic;
using System.Linq;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Repositories
{
    public class TermData : ITermData
    {
        public IList<Term> GetAllTerms()
        {
            using (var db = new Production2016Entities())
            {
                return db.Terms.ToList();
            }
        }

        public IList<TermType> GetAllTermTypes()
        {
            using (var db = new Production2016Entities())
            {
                return db.TermTypes.ToList();
            }
        }
    }
}
