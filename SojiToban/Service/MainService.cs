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
    class PersonalSchedule
    {
        public List<int> dayIndex { get; set; }
        public List<int> placeIndex { get; set; }

        public PersonalSchedule()
        {
            this.dayIndex = new List<int>();
            this.placeIndex = new List<int>();
        }
    }

    class TeamSchedule
    {
        public List<int> dayIndex { get; set; }
        public List<int> placeIndex { get; set; }

        public TeamSchedule()
        {
            this.dayIndex = new List<int>();
            this.placeIndex = new List<int>();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    class MainService
    {
        internal void MainProc(List<Person> list)
        {
            List<List<int>> RandamNumListOfWeek = CreateNumMap();
            Dictionary<int, int> NumberHistory = new Dictionary<int, int>();
            List<int> vlist = new List<int>();
            PersonalSchedule Schedule = new PersonalSchedule();            
            while (true)
            {
                int ScoreA = 0;
                ScoreA = Calculate(RandamNumListOfWeek, Schedule);
                vlist.Add(ScoreA);                
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="RandamNumListOfWeek"></param>
        /// <param name="NumberHistory"></param>
        /// <returns></returns>
        private int Calculate(List<List<int>> RandamNumListOfWeek, PersonalSchedule Schedule)
        {
            System.Random rng = new System.Random();
            List<int> Works = new List<int>();
            int Score = 0;

            while (true)
            {
                int dayIndex = rng.Next(5);
                int placeIndex = rng.Next(RandamNumListOfWeek[dayIndex].Count());

                if (Works.Count < 5 && !Schedule.dayIndex.Contains(dayIndex) && !Schedule.placeIndex.Contains(placeIndex))
                {
                    Schedule.dayIndex.Add(dayIndex);
                    Schedule.placeIndex.Add(placeIndex);
                    int AmountOfWork = ContractConst.COEFFICIENT[RandamNumListOfWeek[dayIndex][placeIndex]];
                    Works.Add(AmountOfWork);
                }
                if (Works.Count == 5)
                {
                    for (int ii = 0; ii < Works.Count; ii++)
                    {
                        Score += Works[ii];
                    }
                    break;
                }
            }
            return Score;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="RandamNumListOfWeek"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private double Variance(List<List<int>> RandamNumListOfWeek, int i, int j)
        {
            int Score1 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[i][j]];
            int Score2 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[0][1]];
            int Score3 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[0][2]];
            Score1 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[1][1]];
            Score2 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[1][2]];
            Score3 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[1][0]];
            Score1 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[2][2]];
            Score2 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[2][0]];
            Score3 = +ContractConst.COEFFICIENT[RandamNumListOfWeek[2][1]];
            var data = new double[] { Score1, Score2, Score3 };
            return data.PopulationVariance();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<List<int>> CreateNumMap()
        {
            List<int> RandamNumListOfDay = new List<int>();
            List<List<int>> RandamNumListOfWeek = new List<List<int>>();
            for (int i = 0; i < ContractConst.WEEK.Count() - 1; i++)
            {
                int[] obj = (int[])ContractConst.WEEK[i];
                RandamNumListOfDay = GetNumListByDay(obj);
                RandamNumListOfWeek.Add(RandamNumListOfDay);
            }
            System.Diagnostics.Debug.WriteLine(RandamNumListOfWeek);
            return RandamNumListOfWeek;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private List<int> GetNumListByDay(int[] p)
        {
            System.Random rng = new System.Random();
            List<int> NumListByDay = new List<int>();
            while (NumListByDay.Count() < p.Count())
            {
                int k = rng.Next(p.Count());
                int num = p[k];
                if (!NumListByDay.Contains(num))
                {
                    NumListByDay.Add(p[k]);
                }
            }
            return NumListByDay;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<Person> RandamSort(List<Person> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Person tmp = list[k];
                list[k] = list[n];
                list[n] = tmp;
            }
            System.Diagnostics.Debug.WriteLine(list);
            return list;
        }
    }

}
