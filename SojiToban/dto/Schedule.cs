using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.dto
{
    public class Schedule
    {
        public int id { get; set; }        
        public Queue<Member> person { get; set; }

        public Schedule()
        {
            this.person = new Queue<Member>();
        }
    }
}
