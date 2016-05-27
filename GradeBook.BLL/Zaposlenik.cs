using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using Csla;
using Csla.Validation;
using GradeBook.BLL.Properties;

namespace GradeBook.BLL
{
    [Serializable()]
    public class Zaposlenik : BusinessBase<Zaposlenik>
    {
        #region Constructors
        private Zaposlenik()
        {

        }
        #endregion

        #region Properties
        private static PropertyInfo<int> IdZaposlenikaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Zaposlenik>(x => x.IdZaposlenika)));

        public int IdZaposlenika
        {
            get { return GetProperty(IdZaposlenikaProperty); }
        }

        private static PropertyInfo<string> ImeZaposlenikaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Zaposlenik>(x => x.ImeZaposlenika)));

        public string ImeZaposlenika
        {
            get { return GetProperty(ImeZaposlenikaProperty); }
            set { SetProperty(ImeZaposlenikaProperty, value); }
        }

        private static PropertyInfo<string> PrezimeZaposlenikaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Zaposlenik>(x => x.PrezimeZaposlenika)));

        public string PrezimeZaposlenika
        {
            get { return GetProperty(PrezimeZaposlenikaProperty); }
            set { SetProperty(PrezimeZaposlenikaProperty, value); }
        }

        private static PropertyInfo<DateTime> DatumPocetkaRadaProperty = RegisterProperty(new PropertyInfo<DateTime>(Reflector.GetPropertyName<Zaposlenik>(x => x.DatumPocetkaRada)));

        public DateTime DatumPocetkaRada
        {
            get { return GetProperty(DatumPocetkaRadaProperty); }
            set { SetProperty(DatumPocetkaRadaProperty, value); }
        }

        private static PropertyInfo<string> OibProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Zaposlenik>(x => x.Oib)));

        public string Oib
        {
            get { return GetProperty(OibProperty); }
            set { SetProperty(OibProperty, value); }
        }

        private static PropertyInfo<string> EmailProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Zaposlenik>(x => x.Email)));

        public string Email
        {
            get { return GetProperty(EmailProperty); }
            set { SetProperty(EmailProperty, value); }
        }

        private static PropertyInfo<int> IdTipaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Zaposlenik>(x => x.IdTipa)));

        public int IdTipa
        {
            get { return GetProperty(IdTipaProperty); }
            set { SetProperty(IdTipaProperty, value); }
        }

        private static PropertyInfo<int> IdSkoleProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Zaposlenik>(x => x.IdSkole)));

        public int IdSkole
        {
            get { return GetProperty(IdSkoleProperty); }
            set { SetProperty(IdSkoleProperty, value); }
        }
        #endregion

        #region Calculated Properties
        public string PunoImeZaposlenika
        {
            get { return ImeZaposlenika + " " + PrezimeZaposlenika; }
        }
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, ImeZaposlenikaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(ImeZaposlenikaProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, PrezimeZaposlenikaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(PrezimeZaposlenikaProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, OibProperty);
            ValidationRules.AddRule(CommonRules.StringMinLength, new CommonRules.MinLengthRuleArgs(OibProperty, 11));
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(OibProperty, 11));

            ValidationRules.AddRule(CommonRules.StringRequired, EmailProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(EmailProperty, 15));
        }

        private static bool IsOibValid<T>(T target, RuleArgs e) where T : Zaposlenik
        {
            if (string.IsNullOrEmpty(target.Oib)) return true;
            try
            {
                Convert.ToInt32(target);
            }
            catch
            {
                e.Description = Resources.InvalidOIB;
                return false;
            }
            return true;
        }

        private static bool IsEmailValid<T>(T target, RuleArgs e) where T : Zaposlenik
        {
            string pattern = "[a-zA-Z0-9]*[\\.]{0,2}[a-zA-Z0-9]*@[a-z]*\\.[a-z]*";
            if (string.IsNullOrEmpty(target.Email)) return true;
            Match result = Regex.Match(target.Email.Trim(), pattern);
            if (result.Success) return true;
            else
            {
                e.Description = Resources.InvalidEmail;
                return false;
            }
        }

        #endregion

        #region Factory Methods
        public static Zaposlenik New()
        {
            return DataPortal.Create<Zaposlenik>();
        }

        public static void Delete(int id)
        {
            DataPortal.Delete<Zaposlenik>(new SingleCriteria<Zaposlenik, int>(id));
        }

        public static Zaposlenik Get(int id)
        {
            return DataPortal.Fetch<Zaposlenik>(new SingleCriteria<Zaposlenik, int>(id));
        }

        //TESTIRANJE
        internal static Zaposlenik Load(DAL.Zaposlenik data)
        {
            return DataPortal.FetchChild<Zaposlenik>(data);
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch(SingleCriteria<Zaposlenik, int> criteria)
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var data = db.DataContext.Zaposlenik.Find(criteria.Value);

                LoadProperty(IdZaposlenikaProperty, data.ID);
                LoadProperty(ImeZaposlenikaProperty, data.ime);
                LoadProperty(PrezimeZaposlenikaProperty, data.prezime);
                LoadProperty(DatumPocetkaRadaProperty, data.datumPocetkaRada);
                LoadProperty(OibProperty, data.oib);
                LoadProperty(EmailProperty, data.email);
                LoadProperty(IdTipaProperty, data.IDtipa);
                LoadProperty(IdSkoleProperty, data.IDskole);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Zaposlenik u = new DAL.Zaposlenik
                {
                    ime = ImeZaposlenika,
                    prezime = PrezimeZaposlenika,
                    datumPocetkaRada = DatumPocetkaRada,
                    oib = Oib,
                    email = Email,
                    IDtipa = IdTipa,
                    IDskole = IdSkole
                };

                db.DataContext.Zaposlenik.Add(u);
                db.DataContext.SaveChanges();

                LoadProperty(IdZaposlenikaProperty, u.ID);

                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Zaposlenik z = db.DataContext.Zaposlenik.Find(IdZaposlenika);
                z.ime = ImeZaposlenika;
                z.prezime = PrezimeZaposlenika;
                z.datumPocetkaRada = DatumPocetkaRada;
                z.oib = Oib;
                z.email = Email;
                z.IDtipa = IdTipa;
                z.IDskole = IdSkole;

                FieldManager.UpdateChildren(this);

                db.DataContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Zaposlenik, int>(IdZaposlenika));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Zaposlenik, int> criteria)
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var z = db.DataContext.Zaposlenik.Find(criteria.Value);
                if(z != null)
                {
                    db.DataContext.Zaposlenik.Remove(z);
                    db.DataContext.SaveChanges();
                }
            }
        }

        //TESTIRANJE
        private void Child_Fetch(DAL.Zaposlenik data)
        {
            LoadProperty(IdZaposlenikaProperty, data.ID);
            LoadProperty(ImeZaposlenikaProperty, data.ime);
            LoadProperty(PrezimeZaposlenikaProperty, data.prezime);
            LoadProperty(DatumPocetkaRadaProperty, data.datumPocetkaRada);
            LoadProperty(OibProperty, data.oib);
            LoadProperty(EmailProperty, data.email);
            LoadProperty(IdTipaProperty, data.IDtipa);
            LoadProperty(IdSkoleProperty, data.IDskole);
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
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(id)).ZaposlenikExists;
            }

            public static bool Exists(string oib)
            {
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(oib)).ZaposlenikExists;
            }
            #endregion

            #region Constructors
            private ExistsCommand(int IdZaposlenika)
            {
                this.idZaposlenika = IdZaposlenika;
            }

            private ExistsCommand(string OibZaposlenika)
            {
                this.oibZaposlenika = OibZaposlenika;
            }

            #endregion

            #region Properties
            public int idZaposlenika { get; private set; }
            public string oibZaposlenika { get; private set; }
            public bool ZaposlenikExists { get; private set; }
            #endregion

            #region Data Access
            #region DataPortal Methods
            protected override void DataPortal_Execute()
            {
                using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
                {
                    ZaposlenikExists = db.DataContext.Skola.Where(x => x.ID == idZaposlenika).Count() > 0;
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
            return GetProperty(IdZaposlenikaProperty).ToString();
        }
        #endregion
    }
}
