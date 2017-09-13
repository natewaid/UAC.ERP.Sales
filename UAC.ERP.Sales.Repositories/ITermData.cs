using System.Collections.Generic;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Repositories
{
    public interface ITermData
    {
        IList<Term> GetAllTerms();
        IList<TermType> GetAllTermTypes();
    }
}