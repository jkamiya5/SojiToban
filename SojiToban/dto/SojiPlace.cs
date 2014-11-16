using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{
    [Serializable]
    [System.Xml.Serialization.XmlRoot("SojiPlace")]
    public class SojiPlace
    {
        [System.Xml.Serialization.XmlElement("m_placeId")]
        public int m_placeId { get; set; }
        [System.Xml.Serialization.XmlElement("m_place")]
        public string m_place { get; set; }
        [System.Xml.Serialization.XmlElement("m_afflictionDegree")]
        public int? m_afflictionDegree { get; set; }
        [System.Xml.Serialization.XmlElement("m_day1")]
        public int? m_day1 { get; set; }
        [System.Xml.Serialization.XmlElement("m_day2")]
        public int? m_day2 { get; set; }
        [System.Xml.Serialization.XmlElement("m_day3")]
        public int? m_day3 { get; set; }
        [System.Xml.Serialization.XmlElement("m_day4")]
        public int? m_day4 { get; set; }
        [System.Xml.Serialization.XmlElement("m_day5")]
        public int? m_day5 { get; set; }
        [System.Xml.Serialization.XmlElement("m_day1_Color")]
        public bool m_day1_Color { get; set; }
        [System.Xml.Serialization.XmlElement("m_day2_Color")]
        public bool m_day2_Color { get; set; }
        [System.Xml.Serialization.XmlElement("m_day3_Color")]
        public bool m_day3_Color { get; set; }
        [System.Xml.Serialization.XmlElement("m_day4_Color")]
        public bool m_day4_Color { get; set; }
        [System.Xml.Serialization.XmlElement("m_day5_Color")]
        public bool m_day5_Color { get; set; }
    }
}
