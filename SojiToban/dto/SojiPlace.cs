using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{
    [Serializable()]
    public class SojiPlace
    {
        public int m_placeId { get; set; }
        public string m_place { get; set; }
        public int? m_afflictionDegree { get; set; }
        public int? m_day1 { get; set; }
        public int? m_day2 { get; set; }
        public int? m_day3 { get; set; }
        public int? m_day4 { get; set; }
        public int? m_day5 { get; set; }
        public bool m_day1_Color { get; set; }
        public bool m_day2_Color { get; set; }
        public bool m_day3_Color { get; set; }
        public bool m_day4_Color { get; set; }
        public bool m_day5_Color { get; set; }
    }
}
