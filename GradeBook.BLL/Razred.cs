using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Csla;
using Csla.Validation;
using GradeBook.BLL.Properties;

namespace GradeBook.BLL
{
    public class Razred : BusinessBase<Razred>
    {
        #region Constructors
        private Razred()
        {

        }
        #endregion

        #region Properties
        private static PropertyInfo<int> IdRazredaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Razred>(x => x.IdRazred)));

        public int IdRazred
        {
            get { return GetProperty(IdRazredaProperty); }
        }

        private static PropertyInfo<string> ImeRazredaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Razred>(x => x.ImeRazreda)));

        public string ImeRazreda
        {
            get { return GetProperty(ImeRazredaProperty); }
            set { SetProperty(ImeRazredaProperty, value); }
        }

        private static PropertyInfo<short> RazinaProperty = RegisterProperty(new PropertyInfo<short>(Reflector.GetPropertyName<Razred>(x => x.Razina)));

        public short Razina
        {
            get { return GetProperty(RazinaProperty); }
            set { SetProperty(RazinaProperty, value); }
        }

        private static PropertyInfo<int> BrojUcenikaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Razred>(x => x.BrojUcenika)));
        
        public int BrojUcenika
        {
            get { return GetProperty(BrojUcenikaProperty); }
            set { SetProperty(BrojUcenikaProperty, value); }
        }

        private static PropertyInfo<int> IDSmjerProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Razred>(x => x.IDSmjer)));

        public int IDSmjer
        {
            get { return GetProperty(IDSmjerProperty); }
            set { SetProperty(IDSmjerProperty, value); }
        }

        private static PropertyInfo<int> IDSkoleProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Razred>(x => x.IDskole)));

        public int IDskole
        {
            get { return GetProperty(IDSkoleProperty); }
            set { SetProperty(IDSkoleProperty, value); }
        }

        #endregion

        #region Calculated Properties
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, ImeRazredaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(ImeRazredaProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, RazinaProperty);
            ValidationRules.AddRule(CommonRules.IntegerMinValue, new CommonRules.IntegerMinValueRuleArgs(RazinaProperty, 1));
            ValidationRules.AddRule(CommonRules.IntegerMaxValue, new CommonRules.IntegerMaxValueRuleArgs(RazinaProperty, 8));

            ValidationRules.AddRule(CommonRules.StringRequired, BrojUcenikaProperty);
            ValidationRules.AddRule(CommonRules.IntegerMinValue, new CommonRules.IntegerMinValueRuleArgs(BrojUcenikaProperty, 1));
            ValidationRules.AddRule(CommonRules.IntegerMaxValue, new CommonRules.IntegerMaxValueRuleArgs(BrojUcenikaProperty, 35));

            ValidationRules.AddRule(CommonRules.StringRequired, IDSmjerProperty);
            ValidationRules.AddRule(CommonRules.StringRequired, IDSkoleProperty);
        }
        #endregion

        #region Factory Methods
        public static Razred New()
        {
            return DataPortal.Create<Razred>();
        }

        public static void Delete(int id)
        {
            DataPortal.Delete<Razred>(new SingleCriteria<Razred, int>(id));
        }

        public static Razred Get(int id)
        {
            return DataPortal.Fetch<Razred>(new SingleCriteria<Razred, int>(id));
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch(SingleCriteria<Razred, int> criteria)
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var data = db.DataContext.Skola.Find(criteria.Value);

                LoadProperty(IdRazredaProperty, data.ID);
                LoadProperty(ImeRazredaProperty, data.nazivSkole);
                LoadProperty(RazinaProperty, data.adresa);
                LoadProperty(BrojUcenikaProperty, data.email);
                LoadProperty(IDSmjerProperty, data.mBrSkole);
                LoadProperty(IDSkoleProperty, data.oibSkole);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Razred r = new DAL.Razred
                {
                    imeRazreda = ImeRazreda,
                    razina = Razina,
                    brojUcenika = BrojUcenika,
                    IDsmjer= IDSmjer,
                    IDskole = IDskole
                };

                db.DataContext.Razred.Add(r);
                db.DataContext.SaveChanges();

                LoadProperty(IdRazredaProperty, r.ID);

                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Razred r = db.DataContext.Razred.Find(IdRazred);
                r.imeRazreda = ImeRazreda;
                r.razina = Razina;
                r.brojUcenika = BrojUcenika;
                r.IDsmjer = IDSmjer;
                r.IDskole = IDskole;

                FieldManager.UpdateChildren(this);

                db.DataContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Razred, int>(IdRazred));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Razred, int> criteria)
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var r = db.DataContext.Razred.Find(criteria.Value);
                if(r != null)
                {
                    db.DataContext.Razred.Remove(r);
                    db.DataContext.SaveChanges();
                }
            }
        }
        #endregion
        #endregion

        #region Exists

        #region ExistsCommand
        [Serializable()]
        private class ExistsCommand : CommandBase
        {
            #region Execute
            public static bool Exists(int id)
            {
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(id)).RazredExists;
            }

            public static bool Exists(string imeRazreda)
            {
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(imeRazreda)).RazredExists;
            }
            #endregion

            #region Constructors
            private ExistsCommand(int IdRazreda)
            {
                this.IdRazreda = IdRazreda;
            }

            private ExistsCommand(string imeRazreda)
            {
                this.ImeRazreda = imeRazreda;
            }

            #endregion

            #region Properties
            public int IdRazreda { get; private set; }
            public string ImeRazreda { get; private set; }
            public bool RazredExists { get; private set; }
            #endregion

            #region Data Access
            #region DataPortal Methods
            protected override void DataPortal_Execute()
            {
                using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
                {
                    RazredExists = db.DataContext.Razred.Where(x => x.ID == IdRazreda).Count() > 0;
                }
            }
            #endregion
            #endregion

        }
        #endregion
        #endregion

        #region Overrides
        public override string ToString()
        {
            return GetProperty(IdRazredaProperty).ToString();
        }
        #endregion
    }
}
