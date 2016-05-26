using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using GradeBook.BLL.Properties;

namespace GradeBook.BLL
{
    [Serializable()]
    public class SkolaInfoList : ReadOnlyListBase<SkolaInfoList, SkolaInfo>
    //dohvaća read-only listu škola (za odabir)
    {
        #region Constructors
        private SkolaInfoList()
        {

        }
        #endregion

        #region Factory Methods
        public static SkolaInfoList Get()
        {
            return DataPortal.Fetch<SkolaInfoList>();
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch()
        {
            this.RaiseListChangedEvents = false;
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                List<SkolaInfo> data = new List<SkolaInfo>();

                foreach (var tz in db.DataContext.Skola.AsNoTracking().ToList())
                    data.Add(new SkolaInfo(tz.ID, tz.nazivSkole));

                IsReadOnly = false;
                this.AddRange(data);
                IsReadOnly = true;
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
        #endregion


    }
}
