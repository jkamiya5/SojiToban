using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using MathNet.Numerics.Statistics;
using SojiToban.CommonModule;
using System.Security.Cryptography;

namespace SojiToban.Service
{


    /// <summary>
    /// メイン処理を行うクラス
    /// </summary>
    class MainService
    {
        /// <summary>
        /// メイン処理
        /// </summary>
        /// <param name="Team"></param>
        /// <returns></returns>
        internal Queue<Member> MainProc(Queue<Member> Team, MainWindow mainWindow)
        {
            DataOption dataOption = new DataOption();
            LoccateOption locateOption = new LoccateOption();

            //清掃箇所をランダムに割り振った数字列作成
            RandamWeekMap RandamWeekMap = dataOption.CreateNumMap();
            int roopCount = 1;

            //曜日毎に割り振りを行う
            foreach (Day EachDay in RandamWeekMap.day)
            {
                bool isHoliday = JudgmentHoliday(mainWindow, EachDay);
                if(isHoliday)
                {
                    continue;
                }

                bool ret = true;                
                //メンバー全員に対して割り振り処理を行う
                while (ret == true)
                {
                    foreach (Member member in Team)
                    {
                        //個人割り振り初回時
                        if (member.day.Count() == 0)
                        {
                            ret = locateOption.AllocationFirstTime(EachDay, member);
                            if (ret == false)
                            {
                                break;
                            }
                        }
                        else
                        {
                            ret = locateOption.Allocation(EachDay, member);
                            if (ret == false)
                            {
                                break;
                            }
                        }
                    }
                    //メンバーのランダムソートを行う
                    if (mainWindow.countRbt.IsChecked == true)
                    {
                        dataOption.RandamSortByCount(ref Team);
                    }
                    else if (mainWindow.scoreRbt.IsChecked == true)
                    {
                        dataOption.RandamSortByScore(ref Team);
                    }
                }
            }
            return Team;
        }

        /// <summary>
        /// 休日設定されている曜日かどうか判定する
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="EachDay"></param>
        private bool JudgmentHoliday(MainWindow mainWindow, Day EachDay)
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
