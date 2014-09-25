using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{

    public class Warifuri
    {
        public static Warifuri instance = null;

        public static Warifuri getInstance()
        {
            if (instance == null)
            {
                instance = new Warifuri();
            }
            return instance;
        }

        public int? day1 { get; set; }
        public int? day2 { get; set; }
        public int? day3 { get; set; }
        public int? day4 { get; set; }
        public int? day5 { get; set; }
    }
}
