using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.DAL
{
    public class ContextManager<C> : IDisposable where C : System.Data.Entity.DbContext
    {
        private C dalContext;
        private string connectionString;
        private static ContextManager<C> dalContextManager;

        private ContextManager(string ConnectionString) // stvara se u GetManager 
        {
            connectionString = ConnectionString;
        }

        public C DataContext
        {
            get
            {
                if (dalContext == null)
                {
                    dalContext = (C)Activator.CreateInstance(typeof(C), connectionString);
                    //dalContext = (C)Activator.CreateInstance(typeof(C));
                }
                return dalContext;
            }
        }

        // stvara ContextManager koji će stvoriti kontekst na temelju connStringa
        public static ContextManager<C> GetManager(string ConnectionString)
        {
            if (dalContextManager == null)
            {
                dalContextManager = new ContextManager<C>(ConnectionString);
            }
            return dalContextManager;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (dalContext != null)
            {
                dalContext.Dispose();
                dalContext = null;
            }

        }
        #endregion
    }
}
