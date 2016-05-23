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
    public class SkolaInfo : ReadOnlyBase<SkolaInfo>
    {
        #region Constructors
        internal SkolaInfo(int id, string naziv)
        {
            IdSkole = id;
            NazivSkole = naziv;
        }
        #endregion

        #region Properties
        public int IdSkole { get; private set; }
        public string NazivSkole { get; private set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return NazivSkole;
        }
        #endregion
    }
}
