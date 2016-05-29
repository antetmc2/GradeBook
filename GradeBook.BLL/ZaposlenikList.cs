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
    public class ZaposlenikList : BusinessListBase<ZaposlenikList, Zaposlenik>
    {
        #region Constructors
        private ZaposlenikList()
        {

        }
        #endregion

        #region Business Methods
        #endregion

        #region Factory Methods
        internal static ZaposlenikList New()
        {
            return DataPortal.CreateChild<ZaposlenikList>();
        }

        internal static ZaposlenikList Load(DAL.Zaposlenik[] data)
        {
            return DataPortal.FetchChild<ZaposlenikList>(data);
        }
        #endregion

        #region Data Access
        private void Child_Fetch(DAL.Zaposlenik[] data)
        {
            RaiseListChangedEvents = false;
            foreach (var child in data) Add(Zaposlenik.Load(child));
            RaiseListChangedEvents = true;
        }
        #endregion
    }
}
