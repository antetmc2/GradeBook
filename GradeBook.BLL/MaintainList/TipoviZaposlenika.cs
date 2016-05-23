using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Core;

namespace GradeBook.BLL.Admin
{
    [Serializable()]
    public class TipoviZaposlenika : BusinessListBase<TipoviZaposlenika, TipZaposlenika>
    {
        #region Constructors
        private TipoviZaposlenika()
        {
            Saved += new EventHandler<SavedEventArgs>(Tipovi_Saved);
            AllowNew = true;
        }
        #endregion

        #region Business Methods
        public void RemoveTipZaposlenika(int id)
        {
            foreach(TipZaposlenika item in this)
            {
                if(item.IdTipa == id)
                {
                    Remove(item);
                    break;
                }
            }
        }

        public TipZaposlenika GetTipZaposlenika(int id)
        {
            foreach(TipZaposlenika item in this)
            {
                if (item.IdTipa == id) return item;
            }
            return null;
        }
        #endregion

        #region Overrides
        protected override object AddNewCore()
        {
            TipZaposlenika item = TipZaposlenika.New();
            this.Add(item);
            return item;
        }
        #endregion

        #region Factory Methods
        public static TipoviZaposlenika Get()
        {
            return DataPortal.Fetch<TipoviZaposlenika>();
        }
        #endregion

        #region Deserialization
        protected override void OnDeserialized()
        {
            base.OnDeserialized();
            Saved += new EventHandler<SavedEventArgs>(Tipovi_Saved);
        }
        #endregion

        #region Cache Invalidation
        // nakon spremanja šifrarnika počisti njegovu kopiju u memoriji (jer je neažurna)
        private void Tipovi_Saved(object sender, SavedEventArgs e)
        {
            TipZaposlenikaList.InvalidateCache();
        }

        protected override void DataPortal_OnDataPortalInvokeComplete(DataPortalEventArgs e)
        {
            TipZaposlenikaList.InvalidateCache();
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                foreach (var tz in db.DataContext.TipZaposlenika)
                    this.Add(TipZaposlenika.Load(tz));
            }
            RaiseListChangedEvents = true;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            RaiseListChangedEvents = false;
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                Child_Update();
            }
            RaiseListChangedEvents = true;
        }
        #endregion
        #endregion
    }
}
