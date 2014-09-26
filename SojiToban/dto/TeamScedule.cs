using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{
    public class TeamSchedule
    {
        public List<int> dayIndex { get; set; }
        public Queue<int> placeIndex { get; set; }
        public int score { get; set; }
        public List<PersonalSchedule> pSchedule { get; set; }
        

        public TeamSchedule()
        {
            this.dayIndex = new List<int>();
            this.placeIndex = new Queue<int>();
            this.score = 0;
            this.pSchedule = new List<PersonalSchedule>();
        }
    }
}
