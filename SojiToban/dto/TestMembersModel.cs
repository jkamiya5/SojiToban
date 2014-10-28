using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.Dto
{
    // DataGridに表示するデータ
    public class TestMembersModel
    {
        [System.Xml.Serialization.XmlElement("tMember")]
        public List<TestMemberModel> Members { get; set; }
    }
}
