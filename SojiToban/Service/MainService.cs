using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using MathNet.Numerics.Statistics;
using SojiToban.CommonModule;

namespace SojiToban.Service
{


    /// <summary>
    /// 
    /// </summary>
    class MainService
    {
        internal void MainProc(Queue<Person> persons)
        {
            RandamWeekMap RandamWeekMap = CreateNumMap();
            Person person = new Person();
            Schedule schedule = new Schedule();
            
            //foreach (var day in RandamWeekMap)
            //{
            //    foreach (var place in day)
            //    {
            //        foreach (var p in persons)
            //        {
            //            if (!GenderAllocationJudge.Judge(p.Gender, place))
            //            {
            //                continue;
            //            }
            //            if (!GenderAllocationJudge.Judge(p.Gender, place))
            //            {
            //                continue;
            //            }
            //        }
            //    }                
            //}
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
        private RandamWeekMap CreateNumMap()
        {
            Day RandamDayMap = new Day();
            RandamWeekMap RandamWeekMap = new RandamWeekMap();
            for (int i = 0; i < ContractConst.WEEK.Count() - 1; i++)
            {
                int[] obj = (int[])ContractConst.WEEK[i];
                RandamDayMap = DayLoccation(obj);
                RandamWeekMap.day.Add(RandamDayMap);
                
            }
            System.Diagnostics.Debug.WriteLine(RandamWeekMap);
            return RandamWeekMap;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Day DayLoccation(int[] p)
        {
            Day day = new Day();
            Place place = new Place();
            while (place.value.Count() < p.Count())
            {
                System.Random rng = new System.Random();
                int k = rng.Next(p.Count());
                int num = p[k];
                if (!place.value.Contains(num))
                {
                    place.value.Enqueue(p[k]);
                }
            }
            day.place.Add(place);
            return day;
        }
    }

}
