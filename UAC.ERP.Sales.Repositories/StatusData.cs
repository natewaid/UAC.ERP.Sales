using System.Collections.Generic;
using System.Linq;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Repositories
{
    public class StatusData : IStatusData
    {
        public IList<Status> GetAllStatuses()
        {
            using (var db = new Production2016Entities())
            {
                return db.Statuses.ToList();
            }
        }
    }
}
