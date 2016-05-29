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
    [Serializable()]
    public class Skola : BusinessBase<Skola>
    {
        #region Constructors
        private Skola()
        {

        }
        #endregion

        #region Properties
        private static PropertyInfo<int> IdSkoleProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Skola>(x => x.IdSkole)));

        public int IdSkole
        {
            get { return GetProperty(IdSkoleProperty); }
        }

        private static PropertyInfo<string> NazivSkoleProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Skola>(x => x.NazivSkole)));

        public string NazivSkole
        {
            get { return GetProperty(NazivSkoleProperty); }
            set { SetProperty(NazivSkoleProperty, value); }
        }

        private static PropertyInfo<string> AdresaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Skola>(x => x.Adresa)));

        public string Adresa
        {
            get { return GetProperty(AdresaProperty); }
            set { SetProperty(AdresaProperty, value); }
        }

        private static PropertyInfo<string> EmailProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Skola>(x => x.Email)));

        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }

        private static PropertyInfo<string> MbrSkoleProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Skola>(x => x.MbrSkole)));

        public string MbrSkole
        {
            get { return GetProperty(MbrSkoleProperty); }
            set { SetProperty(MbrSkoleProperty, value); }
        }

        private static PropertyInfo<string> OibSkoleProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Skola>(x => x.OibSkole)));

        public string OibSkole
        {
            get { return GetProperty(OibSkoleProperty); }
            set { SetProperty(OibSkoleProperty, value); }
        }

        private static PropertyInfo<string> TelefonProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Skola>(x => x.Telefon)));

        public string Telefon
        {
            get { return GetProperty(TelefonProperty); }
            set { SetProperty(TelefonProperty, value); }
        }

        //TESTIRANJE
        private static PropertyInfo<ZaposlenikList> ZaposlenikSkoleProperty = RegisterProperty(typeof(Skola), new PropertyInfo<ZaposlenikList>(Reflector.GetPropertyName<Skola>(x => x.ZaposlenikSkole)));

        public ZaposlenikList ZaposlenikSkole
        {
            get
            {
                if (!(FieldManager.FieldExists(ZaposlenikSkoleProperty)))
                {
                    LoadProperty(ZaposlenikSkoleProperty, ZaposlenikList.New());
                }
                return GetProperty(ZaposlenikSkoleProperty);
            }
        }
        #endregion

        #region Calculated Properties
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, NazivSkoleProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(NazivSkoleProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, AdresaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(AdresaProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, EmailProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(EmailProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, MbrSkoleProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(MbrSkoleProperty, 10));
            ValidationRules.AddRule<Skola>(IsMbrValid, MbrSkoleProperty);

            ValidationRules.AddRule(CommonRules.StringRequired, OibSkoleProperty);
            ValidationRules.AddRule(CommonRules.StringMinLength, new CommonRules.MinLengthRuleArgs(OibSkoleProperty, 11));
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(OibSkoleProperty, 11));
            ValidationRules.AddRule<Skola>(IsOIBValid, OibSkoleProperty);

            ValidationRules.AddRule(CommonRules.StringRequired, TelefonProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(TelefonProperty, 15));
            ValidationRules.AddRule<Skola>(IsTelefonValid, TelefonProperty);
        }

        private static bool IsMbrValid<T>(T target, RuleArgs e) where T : Skola
        {
            if (string.IsNullOrEmpty(target.MbrSkole)) return true;
            try
            {
                Convert.ToInt64(target.MbrSkole);
            }
            catch
            {
                e.Description = Resources.InvalidMbr;
                return false;
            }
            return true;
        }

        private static bool IsOIBValid<T>(T target, RuleArgs e) where T : Skola
        {
            if (string.IsNullOrEmpty(target.OibSkole)) return true;
            try
            {
                Convert.ToInt64(target.OibSkole);
            }
            catch
            {
                e.Description = Resources.InvalidOIB;
                return false;
            }
            return true;
        }

        private static bool IsTelefonValid<T>(T target, RuleArgs e) where T : Skola
        {
            if (string.IsNullOrEmpty(target.Telefon)) return true;
            try
            {
                Convert.ToInt64(target.Telefon);
            }
            catch
            {
                e.Description = Resources.InvalidPhone;
                return false;
            }
            return true;
        }
        #endregion

        #region Factory Methods
        public static Skola New()
        {
            return DataPortal.Create<Skola>();
        }

        public static void Delete(int id)
        {
            DataPortal.Delete<Skola>(new SingleCriteria<Skola, int>(id));
        }

        public static Skola Get(int id)
        {
            return DataPortal.Fetch<Skola>(new SingleCriteria<Skola, int>(id));
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch(SingleCriteria<Skola, int> criteria)
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var data = db.DataContext.Skola.Find(criteria.Value);

                LoadProperty(IdSkoleProperty, data.ID);
                LoadProperty(NazivSkoleProperty, data.nazivSkole);
                LoadProperty(AdresaProperty, data.adresa);
                LoadProperty(EmailProperty, data.email);
                LoadProperty(MbrSkoleProperty, data.mBrSkole);
                LoadProperty(OibSkoleProperty, data.oibSkole);
                LoadProperty(TelefonProperty, data.telefon);

                LoadProperty(ZaposlenikSkoleProperty, ZaposlenikList.Load(data.Zaposlenik.ToArray()));
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Skola s = new DAL.Skola
                {
                    nazivSkole = NazivSkole,
                    adresa = Adresa,
                    email = Email,
                    mBrSkole = MbrSkole,
                    oibSkole = OibSkole,
                    telefon = Telefon
                };

                db.DataContext.Skola.Add(s);
                db.DataContext.SaveChanges();

                LoadProperty(IdSkoleProperty, s.ID);

                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Skola s = db.DataContext.Skola.Find(IdSkole);
                s.nazivSkole = NazivSkole;
                s.adresa = Adresa;
                s.email = Email;
                s.mBrSkole = MbrSkole;
                s.oibSkole = OibSkole;
                s.telefon = Telefon;

                FieldManager.UpdateChildren(this);

                db.DataContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Skola, int>(IdSkole));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Skola, int> criteria)
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var s = db.DataContext.Skola.Find(criteria.Value);
                if(s != null)
                {
                    db.DataContext.Skola.Remove(s);
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
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(id)).SkolaExists;
            }

            public static bool Exists(string oib)
            {
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(oib)).SkolaExists;
            }
            #endregion

            #region Constructors
            private ExistsCommand(int IdSkole)
            {
                this.IdSkole = IdSkole;
            }

            private ExistsCommand(string OibSkole)
            {
                this.OibSkole = OibSkole;
            }

            //private ExistsCommand(string MbrSkole)
            //{
            //    this.MbrSkole = MbrSkole;
            //}
            //ne radi?????
            #endregion

            #region Properties
            public int IdSkole { get; private set; }
            public string OibSkole { get; private set; }
            //public string MbrSkole { get; private set; }
            public bool SkolaExists { get; private set; }
            #endregion
            #region Data Access
            #region DataPortal Methods
            protected override void DataPortal_Execute()
            {
                using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
                {
                    SkolaExists = db.DataContext.Skola.Where(x => x.ID == IdSkole).Count() > 0;
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
            return GetProperty(IdSkoleProperty).ToString();
        }
        #endregion
    }
}
