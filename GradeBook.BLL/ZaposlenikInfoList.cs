using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace GradeBook.BLL
{
    [Serializable()]
    public class ZaposlenikInfoList : ReadOnlyListBase<ZaposlenikInfoList, ZaposlenikInfo>
        //dohvaća read-only listu zaposlenika (za odabir)
    {
        #region Constructors
        private ZaposlenikInfoList()
        {

        }
        #endregion

        #region Factory Methods
        public static ZaposlenikInfoList Get()
        {
            return DataPortal.Fetch<ZaposlenikInfoList>();
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                List<ZaposlenikInfo> data = new List<ZaposlenikInfo>();
                foreach (var z in db.DataContext.Zaposlenik.AsNoTracking().ToList())
                    data.Add(new ZaposlenikInfo(z.ID, z.ime, z.prezime));

                IsReadOnly = false;
                AddRange(data);
                IsReadOnly = true;
            }
            RaiseListChangedEvents = true;
        }
        #endregion
        #endregion
    }
}
