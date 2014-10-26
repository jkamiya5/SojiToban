using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiTobanTest.TestDto
{
        // DataGridに表示するデータ
        public class TestMemberClass
        {
            //ArrayListに追加される型を指定する
            [System.Xml.Serialization.XmlArrayItem(typeof(TestMember))]
            public System.Collections.ArrayList Items;
        }
}
