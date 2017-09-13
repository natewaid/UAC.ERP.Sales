using System.Collections.Generic;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Business.Operation
{
    public interface ITermHelper
    {
        IList<Term> GetAllTerms();
        IList<TermType> GetAllTermTypes();
    }
}