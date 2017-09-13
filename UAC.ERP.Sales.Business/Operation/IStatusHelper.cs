using System.Collections.Generic;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Business.Operation
{
    public interface IStatusHelper
    {
        IList<Status> GetAllStatuses();
    }
}