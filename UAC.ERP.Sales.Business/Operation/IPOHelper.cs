using System.Collections.ObjectModel;
using UAC.ERP.Sales.Business.Models;

namespace UAC.ERP.Sales.Business.Operation
{
    public interface IPOHelper
    {
        ObservableCollection<PoHeadModel> GetAllPos();
        PoHeadModel GetPo(int poId);
        PoLineModel GetPoLine(int poLineId);
        bool SavePoHead(PoHeadModel poHead);
        bool SavePoLine(PoLineModel poLine);
    }
}