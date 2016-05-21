using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.DAL
{
    public partial class risEntities
    {
        // instanciramo nazivom ključa za DAL config
        public risEntities(string ConnectionStringName)
          : base("name=" + ConnectionStringName)
        {
        }
    }
}
