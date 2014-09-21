using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.dto
{
    class SojiPlace
    {
        public string m_placeId { get; set; }
        public string m_place { get; set; }
        public int? m_day1 { get; set; }
        public int? m_day2 { get; set; }
        public int? m_day3 { get; set; }
        public int? m_day4 { get; set; }
        public int? m_day5 { get; set; }
        public bool IsEmpty
        {
            get
            {
                if (m_day1 == 0)
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
