using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.CommonModule
{
    public class HolidayJudge
    {
        /// <summary>
        /// 休日設定されている曜日かどうか判定する
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="EachDay"></param>
        public bool Judge(MainWindow mainWindow, Day EachDay)
        {
            if (mainWindow.chkMon.IsChecked == true && EachDay.days == ContractConst.DAYS.月)
            {
                return true;
            }
            if (mainWindow.chkTue.IsChecked == true && EachDay.days == ContractConst.DAYS.火)
            {
                return true;
            }
            if (mainWindow.chkWed.IsChecked == true && EachDay.days == ContractConst.DAYS.水)
            {
                return true;
            }
            if (mainWindow.chkThu.IsChecked == true && EachDay.days == ContractConst.DAYS.木)
            {
                return true;
            }
            if (mainWindow.chkFri.IsChecked == true && EachDay.days == ContractConst.DAYS.金)
            {
                return true;
            }
            return false;
        }
    }
}
