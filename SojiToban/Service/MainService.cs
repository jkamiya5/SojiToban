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
    class MainService
    {
        internal void execute(List<Person> list)
        {
            List<Person> ret = RandamSort(list);
            List<List<int>> RandamNumListOfWeek = CreateNumMap();
            System.Random rng = new System.Random();
            int k = rng.Next(5);
            List<double> vlist = new List<double>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            List<int> Score = new List<int>();
            List<int> Data = new List<int>();            

            while (true)
            {
                int i = rng.Next(5);
                int j = rng.Next(RandamNumListOfWeek[i].Count());
                //Data.Add(GatScore(RandamNumListOfWeek, dict, i, j, Score));
            }
        }

        private int GatScore(List<List<int>> RandamNumListOfWeek, Dictionary<int, int> dict, int i, int j, List<int> Score)
        {
            int Total = 0;
            if (Score.Count < 3 && !dict.ContainsKey(i) && !dict.ContainsValue(j))
            {
                dict.Add(i, j);
                Score.Add(Const.RANK[RandamNumListOfWeek[i][j]]);
            }
            if (Score.Count == 3)
            {                
                for (int ii = 0; ii < Score.Count; ii++)
                {
                    Total += Score[ii];
                }               
            }
            return Total;
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
            int Score1 = +Const.RANK[RandamNumListOfWeek[i][j]];
            int Score2 = +Const.RANK[RandamNumListOfWeek[0][1]];
            int Score3 = +Const.RANK[RandamNumListOfWeek[0][2]];
            Score1 = +Const.RANK[RandamNumListOfWeek[1][1]];
            Score2 = +Const.RANK[RandamNumListOfWeek[1][2]];
            Score3 = +Const.RANK[RandamNumListOfWeek[1][0]];
            Score1 = +Const.RANK[RandamNumListOfWeek[2][2]];
            Score2 = +Const.RANK[RandamNumListOfWeek[2][0]];
            Score3 = +Const.RANK[RandamNumListOfWeek[2][1]];
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
            for (int i = 0; i < Const.WEEK.Count() - 1; i++)
            {
                int[] obj = (int[])Const.WEEK[i];
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
