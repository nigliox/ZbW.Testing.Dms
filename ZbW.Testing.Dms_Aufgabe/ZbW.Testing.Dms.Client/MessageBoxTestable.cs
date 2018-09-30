using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZbW.Testing.Dms.Client
{
    public class MessageBoxTestable
    {
        public virtual void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
            throw new Exception("Kein Suchbegriff Oder Typ angegeben");
            
        }
    }
}
