using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client
{
    [ExcludeFromCodeCoverage]
    class ProcessTestable
    {
        public virtual void Open(string filePath)
        {
            System.Diagnostics.Process.Start(filePath);
        }
    }
}
