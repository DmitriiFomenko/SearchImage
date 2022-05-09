using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchImage.Classes
{
    class CommandWin
    {
        //https://docs.microsoft.com/ru-ru/dotnet/api/system.windows.forms.sendkeys.send?view=netframework-4.8
        static public void Keys(string keys)
        {
            SendKeys.Send(keys);
        }
    }
}
