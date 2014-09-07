using SojiToban.contract_const;
using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;

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

            while (true)
            {
                int i1 = rng.Next(5);
                int j1 = rng.Next(RandamNumListOfWeek[i1].Count());
                int i2 = rng.Next(5);
                int j2 = rng.Next(RandamNumListOfWeek[i2].Count());
                int i3 = rng.Next(5);
                int j3 = rng.Next(RandamNumListOfWeek[i3].Count());
                int i4 = rng.Next(5);
                int j4 = rng.Next(RandamNumListOfWeek[i4].Count());
                int i5 = rng.Next(5);
                int j5 = rng.Next(RandamNumListOfWeek[i5].Count());
                int Score1 = +Const.RANK[RandamNumListOfWeek[i1][j1]];
                int Score2 = +Const.RANK[RandamNumListOfWeek[i2][j2]];
                int Score3 = +Const.RANK[RandamNumListOfWeek[i3][j3]];
                int Score4 = +Const.RANK[RandamNumListOfWeek[i4][j4]];
                int Score5 = +Const.RANK[RandamNumListOfWeek[i5][j5]];
                int[] Scores = { Score1, Score2, Score3, Score4, Score5 };
            }
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
