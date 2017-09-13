using System.Collections.Generic;
using UAC.ERP.Sales.Models;
using UAC.ERP.Sales.Repositories;

namespace UAC.ERP.Sales.Business.Operation
{
    public class StatusHelper : IStatusHelper
    {
        private IStatusData _statusData;

        public StatusHelper()
        {
            _statusData = new StatusData();
        }

        public IList<Status> GetAllStatuses()
        {
            return _statusData.GetAllStatuses();
        }
    }
}
