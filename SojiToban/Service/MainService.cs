using SojiToban.dto;
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
    /// 
    /// </summary>
    class MainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Team"></param>
        /// <returns></returns>
        internal Queue<Member> MainProc(Queue<Member> Team, MainWindow mainWindow)
        {
            DataOption dataOption = new DataOption();
            LoccateOption locateOption = new LoccateOption();

            //清掃箇所をランダムに割り振った数字列作成
            RandamWeekMap RandamWeekMap = dataOption.CreateNumMap();

            //曜日毎に割り振りを行う
            foreach (Day EachDay in RandamWeekMap.day)
            {
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
        /// 
        /// </summary>
        /// <param name="RandamWeekMap"></param>
        /// <param name="pSchedule"></param>
        /// <param name="tSchedule"></param>
        /// <returns></returns>
        //private TeamSchedule Assign(List<Queue<int>> RandamWeekMap)
        //{
        //    System.Random rng = new System.Random();
        //    List<int> Works = new List<int>();
        //    Schedule tSchedule = new Schedule();            

        //    for (int i = 0; i < RandamWeekMap.Count(); i++ )
        //    {
        //        while (RandamWeekMap[i].Count > 0)
        //        {
        //            PersonalSchedule pSchedule = new PersonalSchedule();
        //            pSchedule.dayIndex.Add(i);
        //            int N = RandamWeekMap[i].Dequeue();
        //            pSchedule.placeIndex.Enqueue(N);
        //            pSchedule.score += ContractConst.COEFFICIENT[N];
        //            tSchedule.pSchedule.Add(pSchedule);                    
        //        }
        //        System.Diagnostics.Debug.WriteLine(tSchedule);
        //    }          
        //    return tSchedule;
        //}

    }

}
