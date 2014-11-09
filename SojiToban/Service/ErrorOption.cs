using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Service
{
    public class ErrorOption : MainWindow
    {
        /// <summary>
        /// エラー処理を行う
        /// </summary>
        /// <param name="p"></param>
        public void ErrorProc(string p)
        {
            this.errorInfo.Content = "  " + p;
        }
    }
}
