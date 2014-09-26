using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{
    public class PersonalSchedule
    {
        public List<int> dayIndex { get; set; }
        public Queue<int?> placeIndex { get; set; }
        public int score { get; set; }

        public PersonalSchedule()
        {
            this.dayIndex = new List<int>();
            this.placeIndex = new Queue<int?>();
            this.score = 0;
        }
    }
}
