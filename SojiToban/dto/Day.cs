using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.dto
{
    public class Day
    {

        public Place P { get; set; }
        public Day()
        {
            this.P = new Place();
        }

    }
}
