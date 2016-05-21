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

            ValidationRules.AddRule(CommonRules.StringRequired, OibSkoleProperty);
            ValidationRules.AddRule(CommonRules.StringMinLength, new CommonRules.MinLengthRuleArgs(OibSkole, 11));
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(OibSkoleProperty, 11));

            ValidationRules.AddRule(CommonRules.StringRequired, TelefonProperty);
            ValidationRules.AddRule(CommonRules.StringMaxLength, new CommonRules.MaxLengthRuleArgs(TelefonProperty, 15));
        }

        private static bool IsMbrValid<T>(T target, RuleArgs e) where T : Skola
        {
            if (string.IsNullOrEmpty(target.MbrSkole)) return true;
            try
            {
                Convert.ToInt32(target);
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
                Convert.ToInt32(target);
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
                Convert.ToInt32(target);
            }
            catch
            {
                e.Description = Resources.InvalidPhone;
                return false;
            }
            return true;
        }
        #endregion
    }
}
