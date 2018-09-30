using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client
{
    internal class FileTestable
    {
        public virtual void Copy(string sourcePath, string destPath)
        {
            File.Copy(sourcePath, destPath);
        }
    }
}
