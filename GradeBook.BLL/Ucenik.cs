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
    public class Ucenik : BusinessBase<Ucenik>
    {
        #region Constructors
        private Ucenik()
        {

        }
        #endregion

        #region Properties
        private static PropertyInfo<int> IdUcenikaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Ucenik>(x => x.IdUcenika)));

        public int IdUcenika
        {
            get { return GetProperty(IdUcenikaProperty); }
        }

        private static PropertyInfo<string> ImeUcenikaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Ucenik>(x => x.ImeUcenika)));

        public string ImeUcenika
        {
            get { return GetProperty(ImeUcenikaProperty); }
            set { SetProperty(ImeUcenikaProperty, value); }
        }

        private static PropertyInfo<string> PrezimeUcenikaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Ucenik>(x => x.PrezimeUcenika)));

        public string PrezimeUcenika
        {
            get { return GetProperty(PrezimeUcenikaProperty); }
            set { SetProperty(PrezimeUcenikaProperty, value); }
        }

        private static PropertyInfo<DateTime> DatumRodjenjaProperty = RegisterProperty(new PropertyInfo<DateTime>(Reflector.GetPropertyName<Ucenik>(x => x.DatumRodjenja)));

        public DateTime DatumRodjenja
        {
            get { return GetProperty(DatumRodjenjaProperty); }
            set { SetProperty(DatumRodjenjaProperty, value); }
        }

        private static PropertyInfo<string> OibProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Ucenik>(x => x.Oib)));

        public string Oib
        {
            get { return GetProperty(OibProperty); }
            set { SetProperty(OibProperty, value); }
        }

        private static PropertyInfo<string> BrojTelefonaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Ucenik>(x => x.BrojTelefona)));

        public string BrojTelefona
        {
            get { return GetProperty(BrojTelefonaProperty); }
            set { SetProperty(BrojTelefonaProperty, value); }
        }

        private static PropertyInfo<string> BrojMobitelaProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Ucenik>(x => x.BrojMobitela)));

        public string BrojMobitela
        {
            get { return GetProperty(BrojMobitelaProperty); }
            set { SetProperty(BrojMobitelaProperty, value); }
        }

        private static PropertyInfo<int> RazredProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Ucenik>(x => x.Razred)));

        public int Razred
        {
            get { return GetProperty(RazredProperty); }
            set { SetProperty(RazredProperty, value); }
        }
        #endregion

        #region Calculated Properties
        public string PunoImeUcenika
        {
            get { return ImeUcenika + " " + PrezimeUcenika; }
        }
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, ImeUcenikaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(ImeUcenikaProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, PrezimeUcenikaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(PrezimeUcenikaProperty, 50));

            ValidationRules.AddRule(CommonRules.StringRequired, DatumRodjenjaProperty);

            ValidationRules.AddRule(CommonRules.StringRequired, OibProperty);
            ValidationRules.AddRule(CommonRules.StringMinLength, new CommonRules.MinLengthRuleArgs(OibProperty, 11));
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(OibProperty, 11));

            ValidationRules.AddRule(CommonRules.StringRequired, BrojTelefonaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(BrojTelefonaProperty, 15));

            ValidationRules.AddRule(CommonRules.StringRequired, BrojMobitelaProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(BrojMobitelaProperty, 18));
        }

        private static bool IsMbrValid<T>(T target, RuleArgs e) where T : Ucenik
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

        private static bool IsTelefonValid<T>(T target, RuleArgs e) where T : Ucenik
        {
            if (string.IsNullOrEmpty(target.BrojTelefona)) return true;
            try
            {
                Convert.ToInt32(target);
            }
            catch
            {
                e.Description = Resources.InvalidPhone;
                return false;
            }
            return true;
        }

        private static bool IsMobitelValid<T>(T target, RuleArgs e) where T : Ucenik
        {
            if (string.IsNullOrEmpty(target.BrojMobitela)) return true;
            try
            {
                Convert.ToInt32(target);
            }
            catch
            {
                e.Description = Resources.InvalidMobitel;
                return false;
            }
            return true;
        }
        #endregion

        #region Factory Methods
        public static Ucenik New()
        {
            return DataPortal.Create<Ucenik>();
        }

        public static void Delete(int id)
        {
            DataPortal.Delete<Ucenik>(new SingleCriteria<Ucenik, int>(id));
        }

        public static Ucenik Get(int id)
        {
            return DataPortal.Fetch<Ucenik>(new SingleCriteria<Ucenik, int>(id));
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch(SingleCriteria<Ucenik, int> criteria)
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var data = db.DataContext.Ucenik.Find(criteria.Value);

                LoadProperty(IdUcenikaProperty, data.ID);
                LoadProperty(ImeUcenikaProperty, data.ime);
                LoadProperty(PrezimeUcenikaProperty, data.prezime);
                LoadProperty(DatumRodjenjaProperty, data.datumRodjenja);
                LoadProperty(OibProperty, data.oib);
                LoadProperty(BrojTelefonaProperty, data.brojTelefona);
                LoadProperty(BrojMobitelaProperty, data.brojMobitela);
                LoadProperty(RazredProperty, data.razred);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Ucenik u = new DAL.Ucenik
                {
                    ime = ImeUcenika,
                    prezime = PrezimeUcenika,
                    datumRodjenja = DatumRodjenja,
                    oib = Oib,
                    brojTelefona = BrojTelefona,
                    brojMobitela = BrojMobitela,
                    razred = Razred
                };

                db.DataContext.Ucenik .Add(u);
                db.DataContext.SaveChanges();

                LoadProperty(IdUcenikaProperty, u.ID);

                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Ucenik s = db.DataContext.Ucenik.Find(IdUcenika);
                s.ime = ImeUcenika;
                s.prezime = PrezimeUcenika;
                s.datumRodjenja = DatumRodjenja;
                s.oib = Oib;
                s.brojTelefona = BrojTelefona;
                s.brojMobitela = BrojMobitela;
                s.razred = Razred;

                FieldManager.UpdateChildren(this);

                db.DataContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Ucenik, int>(IdUcenika));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Ucenik, int> criteria)
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var u = db.DataContext.Ucenik.Find(criteria.Value);
                if(u != null)
                {
                    db.DataContext.Ucenik.Remove(u);
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
            return GetProperty(IdUcenikaProperty).ToString();
        }
        #endregion
    }
}
