using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public abstract class ActionEventArgs : EventArgs
    {
        public Exception Excepcion { get; private set; }
        public bool WithErrors { get { return Excepcion != null; } }

        public ActionEventArgs(Exception ex = null)
        {
            Excepcion = ex;
        }

        public bool ErrorMustBeReThrow { get; set; }
    }
}
