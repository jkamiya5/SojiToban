using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiTobanTest.TestDto
{
    // DataGridに表示するデータ
    public class TestMember
    {
        //ArrayListに追加される型を指定する
        public int? No;
        public string Name;
        public int? Score;
        public string Info;

        public TestMember(string Name, int? No, int? Score)
        {
            this.No = No;
            this.Name = Name;
            this.Score = Score;
            this.Info = "";
        }
    }
}
