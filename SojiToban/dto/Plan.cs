using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.dto
{
    public class Plan
    {
        public int id { get; set; }
        public int varliant { get; set; }
        public List<Schedule> schedule;
    }
}
