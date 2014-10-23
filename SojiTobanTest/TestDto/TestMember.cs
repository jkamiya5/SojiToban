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
            public int? No { get; set; }
            public string Name { get; set; }
            public ContractConst.GENDER? Gender { get; set; }            
            public int? Score { get; set; }
            public string Info { get; set; }
            protected List<Day> day;

            public TestMember()
            {
                this.day = new List<Day>();
                this.Score = 0;
            }
            public void Clear()
            {
                this.day.Clear();
                this.Score = null;
            }

            public TestMember(string Name, int? No, ContractConst.GENDER? Gender, List<Day> day, int? Score)
            {
                this.No = No;
                this.Name = Name;
                this.Gender = Gender;
                this.day = day;
                this.Score = Score;
            }
        }
}
