using System.Collections.Generic;
using UAC.ERP.Sales.Models;
using UAC.ERP.Sales.Repositories;

namespace UAC.ERP.Sales.Business.Operation
{
    public class TermHelper : ITermHelper
    {
        private ITermData _termData;

        public TermHelper()
        {
            _termData = new TermData();
        }

        public IList<Term> GetAllTerms()
        {
            return _termData.GetAllTerms();
        }

        public IList<TermType> GetAllTermTypes()
        {
            return _termData.GetAllTermTypes();
        }
    }
}
