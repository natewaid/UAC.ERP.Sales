using System.Collections.Generic;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Repositories
{
    public interface IPOData
    {
        bool AddNewPoHead(POHead newPoLine);
        bool AddNewPoLine(POLine newPoLine);
        List<POHead> GetAllPos();
        POHead GetPo(int poHeadId);
        POLine GetPoLine(int poLineId);
        bool SaveEditedPoHead(POHead poToSave);
        bool SaveEditedPoLine(POLine poLineToSave);        
    }
}