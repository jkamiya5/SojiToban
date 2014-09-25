﻿using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.Dto
{
        // DataGridに表示するデータ
        public class Member
        {
            public int? No { get; set; }
            public string Name { get; set; }
            public ContractConst.GENDER? Gender { get; set; }
            public List<Day> day;
            public int? Score { get; set; }
            public string Info { get; set; }

            public Member()
            {
                this.day = new List<Day>();
                this.Score = 0;
            }
            public void Clear()
            {
                this.day.Clear();
                this.Score = null;
            }
        }
}
