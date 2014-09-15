using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.dto
{
    public class Schedule
    {        
        public Day D { get; set; }
        public Schedule()
        {
            this.D = new Day();
        }
    }
}
