using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.Dto
{
    // DataGridに表示するデータ
    public class TestMember
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlAttribute("No")]
        public int? No { get; set; }
        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }
        [System.Xml.Serialization.XmlElement("Score")]
        public int? Score { get; set; }
        [System.Xml.Serialization.XmlElement("Info")]
        public string Info { get; set; }
        //男は1、女は2
        [System.Xml.Serialization.XmlElement("Gender")]
        public int Gender { get; set; }
    }
}
