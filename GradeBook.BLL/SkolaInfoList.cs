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
    public class SkolaInfoList : NameValueListBase<int, string>
    {
        #region Constructors
        private SkolaInfoList()
        {

        }
        #endregion

        #region Business Methods
        public static int Default()
        {
            SkolaInfoList sk = Get();
            if (sk.Count > 0) return sk.Items[0].Key;
            else throw new NullReferenceException(Resources.NoDataFound);
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch()
        {
            this.RaiseListChangedEvents = false;
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                List<NameValuePair> data = new List<NameValuePair>();

                foreach (var tz in db.DataContext.Skola.AsNoTracking().ToList())
                    data.Add(new NameValuePair(tz.ID, tz.nazivSkole));

                IsReadOnly = false;
                this.AddRange(data);
                IsReadOnly = true;
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
        #endregion

        #region Factory Methods
        private static SkolaInfoList skole;
        public static SkolaInfoList Get()
        {
            if (skole == null) return DataPortal.Fetch<SkolaInfoList>();
            return skole;
        }
        public static void InvalidateCache()
        {
            skole = null;
        }
        #endregion

    }
}
