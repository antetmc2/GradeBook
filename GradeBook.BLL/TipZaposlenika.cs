using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using GradeBook.BLL.Properties;

namespace GradeBook.BLL
{
    [Serializable()]
    public class TipZaposlenika : NameValueListBase<int, string>
    {
        #region Constructors
        private TipZaposlenika()
        {

        }
        #endregion
        #region Business Methods
        public static int Default()
        {
            TipZaposlenika tip = Get();
            if (tip.Count > 0) return tip.Items[0].Key;
            else throw new NullReferenceException(Resources.NoDataFound);
        }
        #endregion

        #region Factory Methods
        private static TipZaposlenika tip;

        public static TipZaposlenika Get()
        {
            if (tip == null) tip = DataPortal.Fetch<TipZaposlenika>();
            return tip;
        }

        public static void InvalidateCache()
        {
            tip = null;
        }
        #endregion
    }
}
