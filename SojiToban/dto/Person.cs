using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SojiToban.dto
{
        // DataGridに表示するデータ
        public class Person
        {
            public string Name { get; set; }
            public Gender? Gender { get; set; }            
            public int? No { get; set; }
            public int? Kbn1 { get; set; }
            public Rireki rireki { get; set; }
        }
}
