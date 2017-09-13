using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Repositories
{
    public class POData : IPOData
    {
        public List<POHead> GetAllPos()
        {
            using (var db = new Production2016Entities())
            {
                return db.POHeads.Include(p => p.POLines).ToList();
            }
        }

        public POHead GetPo(int poHeadId)
        {
            using (var db = new Production2016Entities())
            {
                return db.POHeads
                    .Include(p => p.POLines)
                    .Single(p => p.POHeadID.Equals(poHeadId));
            }
        }

        public bool AddNewPoHead(POHead newPoLine)
        {
            using (var db = new Production2016Entities())
            {
                try
                {
                    db.POHeads.Add(newPoLine);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool SaveEditedPoHead(POHead poToSave)
        {
            using (var db = new Production2016Entities())
            {
                var po = db.POHeads.Single(p => p.POHeadID.Equals(poToSave.POHeadID));
                po = poToSave.CopyTo(po);

                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public POLine GetPoLine(int poLineId)
        {
            using (var db = new Production2016Entities())
            {
                return db.POLines.Single(p => p.POLineId.Equals(poLineId));
            }
        }

        public bool AddNewPoLine(POLine newPoLine)
        {
            using (var db = new Production2016Entities())
            {
                try
                {
                    db.POLines.Add(newPoLine);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }        

        public bool SaveEditedPoLine(POLine poLineToSave)
        {
            using (var db = new Production2016Entities())
            {
                var line = db.POLines.Single(p => p.POLineId.Equals(poLineToSave.POLineId));
                line = poLineToSave.CopyTo(line);

                try
                {                    
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }        
    }
}
