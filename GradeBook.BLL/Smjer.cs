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
    public class Smjer : NameValueListBase<int, string>
    {
        #region Constructors
        private Smjer()
        {

        }
        #endregion
        #region Business Methods
        public static int Default()
        {
            Smjer smjer = Get();
            if (smjer.Count > 0) return smjer.Items[0].Key;
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

                foreach (var s in db.DataContext.Smjer.AsNoTracking().ToList())
                    data.Add(new NameValuePair(s.ID, s.imeSmjera));

                IsReadOnly = false;
                this.AddRange(data);
                IsReadOnly = true;
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
        #endregion
        #region Factory Methods
        private static Smjer smjer;

        public static Smjer Get()
        {
            if (smjer == null) smjer = DataPortal.Fetch<Smjer>();
            return smjer;
        }

        public static void InvalidateCache()
        {
            smjer = null;
        }
        #endregion
    }
}
