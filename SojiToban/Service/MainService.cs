using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.Service
{
    class MainService
    {
        internal void execute(List<Person> list)
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
        }
    }

}
