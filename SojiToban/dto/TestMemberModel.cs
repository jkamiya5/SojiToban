using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.Dto
{
    // DataGridに表示するデータ
    public class TestMemberModel
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlAttribute("No")]
        public string No { get; set; }
        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }
        [System.Xml.Serialization.XmlElement("Score")]
        public int? Score { get; set; }
        [System.Xml.Serialization.XmlElement("Info")]
        public string Info { get; set; }
        [System.Xml.Serialization.XmlElement("Gender")]
        //男は「0」、女は「1」
        public int Gender { get; set; }
    }
}
