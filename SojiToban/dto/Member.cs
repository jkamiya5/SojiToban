using SojiToban.CommonModule;
using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.Dto
{
    // DataGridに表示するデータ
    [Serializable]
    public class Member
    {
        public int? No { get; set; }
        public string Name { get; set; }
        public ContractConst.GENDER? Gender { get; set; }
        public int? Score { get; set; }
        public string Info { get; set; }
        public List<Day> day;

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

        public Member(string Name, int? No, ContractConst.GENDER? Gender, List<Day> day, int? Score)
        {
            this.No = No;
            this.Name = Name;
            this.Gender = Gender;
            this.day = day;
            this.Score = Score;
        }
    }
}
