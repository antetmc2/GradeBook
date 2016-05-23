using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Validation;
using System.Reflection;

namespace GradeBook.BLL.Admin
{
    [Serializable()]
    public class TipZaposlenika : BusinessBase<TipZaposlenika>
    {
        #region Constructors
        private TipZaposlenika()
        {

        }
        #endregion

        #region Properties
        private bool idSet;
        private static PropertyInfo<int> IdTipaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<TipZaposlenika>(x => x.IdTipa)));

        public int IdTipa
        {
            get
            {
                if(!idSet)
                {
                    idSet = true;
                    SetProperty(IdTipaProperty, GetMax() + 1);
                }
                return GetProperty(IdTipaProperty);
            }
            set
            {
                idSet = true;
                SetProperty(IdTipaProperty, value);
            }
        }

        private static PropertyInfo<string> NazivTipaProperty = RegisterProperty(typeof(TipZaposlenika), new PropertyInfo<string>(Reflector.GetPropertyName<TipZaposlenika>(x => x.NazivTipa)));

        public string NazivTipa
        {
            get { return GetProperty(NazivTipaProperty); }
            set { SetProperty(NazivTipaProperty, value); }
        }
        #endregion

        #region Business Methods
        private int GetMax()
        {
            TipoviZaposlenika parent = (TipoviZaposlenika)Parent;
            int max = parent.Max(x => x.IdTipa);
            return max > 0 ? max : 0;
        }
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, NazivTipaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(NazivTipaProperty, 50));
        }
        #endregion

        #region Factory Methods
        internal static TipZaposlenika New()
        {
            return DataPortal.CreateChild<TipZaposlenika>();
        }

        internal static TipZaposlenika Load(DAL.TipZaposlenika data)
        {
            return DataPortal.FetchChild<TipZaposlenika>(data);
        }
        #endregion

        #region Data Access
        private void Child_Fetch(DAL.TipZaposlenika data)
        {
            LoadProperty(IdTipaProperty, data.ID);
            LoadProperty(NazivTipaProperty, data.nazivTipa);
            idSet = true;
        }

        private void Child_Insert()
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.TipZaposlenika tz = new DAL.TipZaposlenika
                {
                    ID = IdTipa,
                    nazivTipa = NazivTipa
                };

                db.DataContext.TipZaposlenika.Add(tz);
                db.DataContext.SaveChanges();
            }
        }

        private void Child_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.TipZaposlenika tz = db.DataContext.TipZaposlenika.Find(IdTipa);

                tz.nazivTipa = NazivTipa;

                db.DataContext.SaveChanges();
            }
        }

        private void Child_DeleteSelf()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.TipZaposlenika tz = db.DataContext.TipZaposlenika.Find(IdTipa);
                if(tz != null)
                {
                    db.DataContext.TipZaposlenika.Remove(tz);
                    db.DataContext.SaveChanges();
                }
            }
        }
        #endregion

        #region Exists
        #endregion
    }
}
