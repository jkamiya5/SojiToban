using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.Dto
{
        // DataGridに表示するデータ
        [System.Xml.Serialization.XmlRoot("TestMemberClass")]
        public class TestMemberClass
        {
            //ArrayListに追加される型を指定する
            [System.Xml.Serialization.XmlElement("TestMember")]
            public System.Collections.Generic.List<TestMember> Items { get; set; }
        }
}
