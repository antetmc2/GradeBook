using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace GradeBook.BLL
{
    //read-only informacije o zaposleniku
    [Serializable()]
    public class ZaposlenikInfo : ReadOnlyBase<ZaposlenikInfo>
    {
        #region Constructors
        internal ZaposlenikInfo(int id, string ime, string prezime)
        {
            IdZaposlenika = id;
            NazivOsobe = String.Format("{0} {1}", ime, prezime);
        }
        #endregion

        #region Properties
        public int IdZaposlenika { get; private set; }
        public string NazivOsobe { get; private set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return NazivOsobe;
        }
        #endregion
    }
}
