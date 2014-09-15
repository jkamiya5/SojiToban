using SojiToban.contract_const;
using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using MathNet.Numerics.Statistics;

namespace SojiToban.Service
{


    /// <summary>
    /// 
    /// </summary>
    class MainService
    {
        internal void MainProc(List<Person> list)
        {
            List<Queue<int>> RandamWeekMap = CreateNumMap();
            Dictionary<int, int> NumberHistory = new Dictionary<int, int>();
            List<int> vlist = new List<int>();
            PersonalSchedule pSchedule = new PersonalSchedule();
            //TeamSchedule tSchedule = Assign(RandamWeekMap);
            //System.Diagnostics.Debug.WriteLine(tSchedule);
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Queue<int>> CreateNumMap()
        {
            Queue<int> RandamDayMap = new Queue<int>();
            List<Queue<int>> RandamWeekMap = new List<Queue<int>>();
            for (int i = 0; i < ContractConst.WEEK.Count() - 1; i++)
            {
                int[] obj = (int[])ContractConst.WEEK[i];
                RandamDayMap = DayLoccation(obj);
                RandamWeekMap.Add(RandamDayMap);
            }
            System.Diagnostics.Debug.WriteLine(RandamWeekMap);
            return RandamWeekMap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Queue<int> DayLoccation(int[] p)
        {            
            Queue<int> RandamDayMap = new Queue<int>();
            while (RandamDayMap.Count() < p.Count())
            {
                System.Random rng = new System.Random();
                int k = rng.Next(p.Count());
                int num = p[k];
                if (!RandamDayMap.Contains(num))
                {
                    RandamDayMap.Enqueue(p[k]);
                }
            }
            return RandamDayMap;
        }
    }

}
