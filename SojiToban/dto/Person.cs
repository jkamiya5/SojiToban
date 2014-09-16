using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.dto
{
        // DataGridに表示するデータ
        public class Member
        {
            public int? No { get; set; }
            public int? Kbn1 { get; set; }
            public string Name { get; set; }
            public ContractConst.GENDER? Gender { get; set; }
            public List<Day> day;
            public int? score { get; set; }

            public Member()
            {
                this.day = new List<Day>();
            }
        }
}
