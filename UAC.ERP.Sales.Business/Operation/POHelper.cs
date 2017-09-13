using System.Collections.ObjectModel;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.Models;
using UAC.ERP.Sales.Repositories;

namespace UAC.ERP.Sales.Business.Operation
{
    public class POHelper : IPOHelper
    {
        private IPOData _poData;

        public POHelper()
        {
            _poData = new POData();
        }

        public ObservableCollection<PoHeadModel> GetAllPos()
        {
            var poList = new ObservableCollection<PoHeadModel>();

            _poData.GetAllPos()
                .ForEach(po =>
                    poList.Add(po.CopyTo(new PoHeadModel { POLines = po.POLines.ToObservableCollection() }))
                );

            return poList;
        }

        public PoHeadModel GetPo(int poId)
        {
            var po = _poData.GetPo(poId);
            var model = po.CopyTo(new PoHeadModel());
            model.POLines = po.POLines.ToObservableCollection();
            return model;
        }

        public PoLineModel GetPoLine(int poLineId)
        {
            var poLine = _poData.GetPoLine(poLineId);
            return poLine.CopyTo(new PoLineModel());
        }

        public bool SavePoHead(PoHeadModel poHead)
        {
            if (poHead.POHeadID == 0)
            {
                return _poData.AddNewPoHead(poHead.CopyTo(new POHead()));
            }

            return _poData.SaveEditedPoHead(poHead.CopyTo(new POHead()));
        }

        public bool SavePoLine(PoLineModel poLine)
        {
            if (poLine.POLineId == 0)
            {
                return _poData.AddNewPoLine(poLine.CopyTo(new POLine()));
            }

            return _poData.SaveEditedPoLine(poLine.CopyTo(new POLine()));
        }                
    }
}
