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
    public class Predmet : BusinessBase<Predmet>
    {
        #region Constructors
        private Predmet()
        {

        }
        #endregion

        #region Properties
        private static PropertyInfo<int> IdPredmetaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Predmet>(x => x.IdPredmeta)));

        public int IdPredmeta
        {
            get { return GetProperty(IdPredmetaProperty); }
        }

        private static PropertyInfo<string> ImePredmetaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Predmet>(x => x.ImePredmeta)));

        public string ImePredmeta
        {
            get { return GetProperty(ImePredmetaProperty); }
            set { SetProperty(ImePredmetaProperty, value); }
        }

        private static PropertyInfo<short> RazinaRazredaProperty = RegisterProperty(new PropertyInfo<short>(Reflector.GetPropertyName<Predmet>(x => x.RazinaRazreda)));

        public short RazinaRazreda
        {
            get { return GetProperty(RazinaRazredaProperty); }
            set { SetProperty(RazinaRazredaProperty, value); }
        }

        private static PropertyInfo<short> SatiTjednoProperty = RegisterProperty(new PropertyInfo<short>(Reflector.GetPropertyName<Predmet>(x => x.SatiTjedno)));

        public short SatiTjedno
        {
            get { return GetProperty(SatiTjednoProperty); }
            set { SetProperty(SatiTjednoProperty, value); }
        }

        private static PropertyInfo<bool> JeObavezanProperty = RegisterProperty(new PropertyInfo<bool>(Reflector.GetPropertyName<Predmet>(x => x.JeObavezan)));

        public bool JeObavezan
        {
            get { return GetProperty(JeObavezanProperty); }
            set { SetProperty(JeObavezanProperty, value); }
        }

        #endregion

        #region Calculated Properties
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, ImePredmetaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(ImePredmetaProperty, 30));

            ValidationRules.AddRule(CommonRules.StringRequired, RazinaRazredaProperty);
            ValidationRules.AddRule(CommonRules.IntegerMinValue, new CommonRules.MinLengthRuleArgs(RazinaRazredaProperty, 1));
            ValidationRules.AddRule(CommonRules.IntegerMaxValue, new CommonRules.IntegerMaxValueRuleArgs(RazinaRazredaProperty, 8));

            ValidationRules.AddRule(CommonRules.StringRequired, SatiTjednoProperty);
            ValidationRules.AddRule(CommonRules.IntegerMinValue, new CommonRules.MinLengthRuleArgs(SatiTjednoProperty, 1));

            ValidationRules.AddRule(CommonRules.StringRequired, JeObavezanProperty);
        }
        #endregion

        #region Factory Methods
        public static Predmet New()
        {
            return DataPortal.Create<Predmet>();
        }

        public static void Delete(int id)
        {
            DataPortal.Delete<Predmet>(new SingleCriteria<Predmet, int>(id));
        }

        public static Predmet Get(int id)
        {
            return DataPortal.Fetch<Predmet>(new SingleCriteria<Predmet, int>(id));
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch(SingleCriteria<Predmet, int> criteria)
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var data = db.DataContext.Predmet.Find(criteria.Value);

                LoadProperty(IdPredmetaProperty, data.ID);
                LoadProperty(ImePredmetaProperty, data.imePredmeta);
                LoadProperty(RazinaRazredaProperty, data.razinaRazreda);
                LoadProperty(SatiTjednoProperty, data.satiTjedno);
                LoadProperty(JeObavezanProperty, data.jeObavezan);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Predmet p = new DAL.Predmet
                {
                    imePredmeta = ImePredmeta,
                    razinaRazreda = RazinaRazreda,
                    satiTjedno = SatiTjedno,
                    jeObavezan = JeObavezan,
                };

                db.DataContext.Predmet.Add(p);
                db.DataContext.SaveChanges();

                LoadProperty(IdPredmetaProperty, p.ID);

                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Predmet s = db.DataContext.Predmet.Find(IdPredmeta);
                s.imePredmeta = ImePredmeta;
                s.razinaRazreda = RazinaRazreda;
                s.satiTjedno = SatiTjedno;
                s.jeObavezan = JeObavezan;

                FieldManager.UpdateChildren(this);

                db.DataContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Predmet, int>(IdPredmeta));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Predmet, int> criteria)
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var p = db.DataContext.Predmet.Find(criteria.Value);
                if(p != null)
                {
                    db.DataContext.Predmet.Remove(p);
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
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(id)).PredmetExists;
            }

            #endregion

            #region Constructors
            private ExistsCommand(int IdPredmeta)
            {
                this.idPredmeta = IdPredmeta;
            }

            #endregion

            #region Properties
            public int idPredmeta { get; private set; }
            public bool PredmetExists { get; private set; }
            #endregion
            #region Data Access
            #region DataPortal Methods
            protected override void DataPortal_Execute()
            {
                using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
                {
                    PredmetExists = db.DataContext.Predmet.Where(x => x.ID == idPredmeta).Count() > 0;
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
            return GetProperty(IdPredmetaProperty).ToString();
        }
        #endregion
    }
}
