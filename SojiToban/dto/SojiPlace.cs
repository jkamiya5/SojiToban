using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.dto
{
    class SojiPlace
    {
        public string PlaceId { get; set; }
        public string Place { get; set; }
        public int? day1 { get; set; }
        public int? day2 { get; set; }
        public int? day3 { get; set; }
        public int? day4 { get; set; }
        public int? day5 { get; set; }
        public bool IsEmpty
        {
            get
            {
                if (day1 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
