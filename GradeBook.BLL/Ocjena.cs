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
    public class Ocjena : BusinessBase<Ocjena>
    {
        #region Constructors
        private Ocjena()
        {

        }
        #endregion

        #region Properties
        private static PropertyInfo<int> IdOcjeneProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Ocjena>(x => x.IdOcjene)));

        public int IdOcjene
        {
            get { return GetProperty(IdOcjeneProperty); }
        }

        private static PropertyInfo<int> IdUcenikaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Ocjena>(x => x.IdUcenika)));

        public int IdUcenika
        {
            get { return GetProperty(IdUcenikaProperty); }
            set { SetProperty(IdUcenikaProperty, value); }
        }

        private static PropertyInfo<int> IdRubrikeProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Ocjena>(x => x.IdRubrike)));

        public int IdRubrike
        {
            get { return GetProperty(IdRubrikeProperty); }
            set { SetProperty(IdRubrikeProperty, value); }
        }

        private static PropertyInfo<short> UnesenaOcjenaProperty = RegisterProperty(new PropertyInfo<short>(Reflector.GetPropertyName<Ocjena>(x => x.UnesenaOcjena)));

        public short UnesenaOcjena
        {
            get { return GetProperty(UnesenaOcjenaProperty); }
            set { SetProperty(UnesenaOcjenaProperty, value); }
        }

        private static PropertyInfo<string> BiljeskaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Ocjena>(x => x.Biljeska)));

        public string Biljeska
        {
            get { return GetProperty(BiljeskaProperty); }
            set { SetProperty(BiljeskaProperty, value); }
        }

        private static PropertyInfo<DateTime> VrijemeUnosaProperty = RegisterProperty(new PropertyInfo<DateTime>(Reflector.GetPropertyName<Ocjena>(x => x.VrijemeUnosa)));

        public DateTime VrijemeUnosa
        {
            get { return GetProperty(VrijemeUnosaProperty); }
            set { SetProperty(VrijemeUnosaProperty, value); }
        }
        #endregion

        #region Calculated Properties
        public string OcjenaInfo
        {
            get { return UnesenaOcjena + " - " + Biljeska + "(" + VrijemeUnosa + ")"; }
        }
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, IdUcenikaProperty);

            ValidationRules.AddRule(CommonRules.StringRequired, IdRubrikeProperty);

            ValidationRules.AddRule(CommonRules.StringRequired, UnesenaOcjenaProperty);
            ValidationRules.AddRule(CommonRules.IntegerMinValue, new CommonRules.IntegerMinValueRuleArgs(UnesenaOcjenaProperty, 1));
            ValidationRules.AddRule(CommonRules.IntegerMaxValue, new CommonRules.IntegerMaxValueRuleArgs(UnesenaOcjenaProperty, 5));

            ValidationRules.AddRule(CommonRules.StringRequired, BiljeskaProperty);
            ValidationRules.AddRule(CommonRules.StringMinLength, new CommonRules.MinLengthRuleArgs(BiljeskaProperty, 1));
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(BiljeskaProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, VrijemeUnosaProperty);

        }
        #endregion

        #region Factory Methods
        public static Ocjena New()
        {
            return DataPortal.Create<Ocjena>();
        }

        public static void Delete(int id)
        {
            DataPortal.Delete<Ocjena>(new SingleCriteria<Ocjena, int>(id));
        }

        public static Ocjena Get(int id)
        {
            return DataPortal.Fetch<Ocjena>(new SingleCriteria<Ocjena, int>(id));
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch(SingleCriteria<Ocjena, int> criteria)
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var data = db.DataContext.Ocjena.Find(criteria.Value);

                LoadProperty(IdOcjeneProperty, data.ID);
                LoadProperty(IdUcenikaProperty, data.IDucenika);
                LoadProperty(IdRubrikeProperty, data.IDrubrike);
                LoadProperty(UnesenaOcjenaProperty, data.ocjena1);
                LoadProperty(BiljeskaProperty, data.biljeska);
                LoadProperty(VrijemeUnosaProperty, data.vrijemeUnosa);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Ocjena o = new DAL.Ocjena
                {
                    IDucenika = IdUcenika,
                    IDrubrike = IdRubrike,
                    ocjena1 = UnesenaOcjena,
                    biljeska = Biljeska,
                    vrijemeUnosa = VrijemeUnosa
                };

                db.DataContext.Ocjena.Add(o);
                db.DataContext.SaveChanges();

                LoadProperty(IdOcjeneProperty, o.ID);

                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Ocjena s = db.DataContext.Ocjena.Find(IdOcjene);
                s.IDucenika = IdUcenika;
                s.IDrubrike = IdRubrike;
                s.ocjena1 = UnesenaOcjena;
                s.biljeska = Biljeska;
                s.vrijemeUnosa = VrijemeUnosa;

                FieldManager.UpdateChildren(this);

                db.DataContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Ocjena, int>(IdOcjene));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Ocjena, int> criteria)
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var o = db.DataContext.Ocjena.Find(criteria.Value);
                if(o != null)
                {
                    db.DataContext.Ocjena.Remove(o);
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
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(id)).OcjenaExists;
            }
            #endregion

            #region Constructors
            private ExistsCommand(int IdOcjene)
            {
                this.IDocjene = IdOcjene;
            }
            #endregion

            #region Properties
            public int IDocjene { get; private set; }
            public bool OcjenaExists { get; private set; }
            #endregion

            #region Data Access
            #region DataPortal Methods
            protected override void DataPortal_Execute()
            {
                using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
                {
                    OcjenaExists = db.DataContext.Ocjena.Where(x => x.ID == IDocjene).Count() > 0;
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
            return GetProperty(IdOcjeneProperty).ToString();
        }
        #endregion
    }
}
