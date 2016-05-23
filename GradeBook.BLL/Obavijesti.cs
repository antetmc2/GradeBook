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
    public class Obavijesti : BusinessBase<Obavijesti>
    {
        #region Constructors
        private Obavijesti()
        {

        }
        #endregion

        #region Properties
        private static PropertyInfo<int> IdObavijestiProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Obavijesti>(x => x.IdObavijesti)));

        public int IdObavijesti
        {
            get { return GetProperty(IdObavijestiProperty); }
        }

        private static PropertyInfo<string> TekstObavijestiProperty = RegisterProperty(new PropertyInfo<string>(Reflector.GetPropertyName<Obavijesti>(x => x.TekstObavijesti)));

        public string TekstObavijesti
        {
            get { return GetProperty(TekstObavijestiProperty); }
            set { SetProperty(TekstObavijestiProperty, value); }
        }

        private static PropertyInfo<DateTime> VrijemeObjavljivanjaProperty = RegisterProperty(new PropertyInfo<DateTime>(Reflector.GetPropertyName<Obavijesti>(x => x.VrijemeObjavljivanja)));

        public DateTime VrijemeObjavljivanja
        {
            get { return GetProperty(VrijemeObjavljivanjaProperty); }
            set { SetProperty(VrijemeObjavljivanjaProperty, value); }
        }

        private static PropertyInfo<int> IdObjavljivacaProperty = RegisterProperty(new PropertyInfo<int>(Reflector.GetPropertyName<Obavijesti>(x => x.IdObjavljivaca)));

        public int IdObjavljivaca
        {
            get { return GetProperty(IdObjavljivacaProperty); }
            set { SetProperty(IdObjavljivacaProperty, value); }
        }
        #endregion

        #region Calculated Properties
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, TekstObavijestiProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(TekstObavijestiProperty, 4000));

            ValidationRules.AddRule(CommonRules.StringRequired, VrijemeObjavljivanjaProperty);

            ValidationRules.AddRule(CommonRules.StringRequired, IdObjavljivacaProperty);
        }
        #endregion

        #region Factory Methods
        public static Obavijesti New()
        {
            return DataPortal.Create<Obavijesti>();
        }

        public static void Delete(int id)
        {
            DataPortal.Delete<Obavijesti>(new SingleCriteria<Obavijesti, int>(id));
        }

        public static Obavijesti Get(int id)
        {
            return DataPortal.Fetch<Obavijesti>(new SingleCriteria<Obavijesti, int>(id));
        }
        #endregion

        #region Data Access
        #region DataPortal Methods
        private void DataPortal_Fetch(SingleCriteria<Obavijesti, int> criteria)
        {
            using(var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var data = db.DataContext.Obavijesti.Find(criteria.Value);

                LoadProperty(IdObavijestiProperty, data.ID);
                LoadProperty(TekstObavijestiProperty, data.tekst);
                LoadProperty(VrijemeObjavljivanjaProperty, data.vrijemeObjavljivanja);
                LoadProperty(IdObjavljivacaProperty, data.IDobjavljivaca);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Obavijesti o = new DAL.Obavijesti
                {
                    tekst = TekstObavijesti,
                    vrijemeObjavljivanja = VrijemeObjavljivanja,
                    IDobjavljivaca = IdObjavljivaca
                };

                db.DataContext.Obavijesti.Add(o);
                db.DataContext.SaveChanges();

                LoadProperty(IdObavijestiProperty, o.ID);

                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                DAL.Obavijesti s = db.DataContext.Obavijesti.Find(IdObavijesti);
                s.tekst = TekstObavijesti;
                s.vrijemeObjavljivanja = VrijemeObjavljivanja;
                s.IDobjavljivaca = IdObjavljivaca;

                FieldManager.UpdateChildren(this);

                db.DataContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Obavijesti, int>(IdObavijesti));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Obavijesti, int> criteria)
        {
            using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
            {
                var o = db.DataContext.Obavijesti.Find(criteria.Value);
                if(o != null)
                {
                    db.DataContext.Obavijesti.Remove(o);
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
                return DataPortal.Execute<ExistsCommand>(new ExistsCommand(id)).ObavijestExists;
            }

            #endregion

            #region Constructors
            private ExistsCommand(int IdObavijesti)
            {
                this.IDobavijesti = IdObavijesti;
            }

            #endregion

            #region Properties
            public int IDobavijesti { get; private set; }
            public bool ObavijestExists { get; private set; }
            #endregion

            #region Data Access
            #region DataPortal Methods
            protected override void DataPortal_Execute()
            {
                using (var db = DAL.ContextManager<DAL.risEntities>.GetManager(DAL.Database.ProjektConnectionString))
                {
                    ObavijestExists = db.DataContext.Obavijesti.Where(x => x.ID == IDobavijesti).Count() > 0;
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
            return GetProperty(IdObavijestiProperty).ToString();
        }
        #endregion
    }
}
