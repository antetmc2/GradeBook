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
    public class TipZaposlenikaList : NameValueListBase<int, string>
    {
        //Read-Only lista za tipa zaposlenika
        #region Constructors
        private TipZaposlenikaList()
        {

        }
        #endregion
        #region Business Methods
        public static int Default()
        {
            TipZaposlenikaList tip = Get();
            if (tip.Count > 0) return tip.Items[0].Key;
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

                foreach (var tz in db.DataContext.TipZaposlenika.AsNoTracking().ToList())
                    data.Add(new NameValuePair(tz.ID, tz.nazivTipa));

                IsReadOnly = false;
                this.AddRange(data);
                IsReadOnly = true;
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
        #endregion
        #region Factory Methods
        private static TipZaposlenikaList tip;

        public static TipZaposlenikaList Get()
        {
            if (tip == null) tip = DataPortal.Fetch<TipZaposlenikaList>();
            return tip;
        }

        public static void InvalidateCache()
        {
            tip = null;
        }
        #endregion
    }
}
