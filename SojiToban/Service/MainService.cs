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
            DataGenerateClass dataOption = new DataGenerateClass();
            LoccateOption locateOption = new LoccateOption();

            //清掃箇所をランダムに割り振った数字列作成
            RandamWeekMap RandamWeekMap = dataOption.CreateNumMap();

            //曜日毎に割り振りを行う
            foreach (Day EachDay in RandamWeekMap.day)
            {
                //休日判定を行う
                HolidayJudge HolidayJudge = new HolidayJudge();
                bool isHoliday = HolidayJudge.Judge(mainWindow, EachDay);

                //休日なら次の曜日へ
                if(isHoliday)
                {
                    continue;
                }


                //割り振り処理フラグ初期化
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
                            ret = locateOption.AssignmentEachDay(EachDay, member);
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

    }
}
