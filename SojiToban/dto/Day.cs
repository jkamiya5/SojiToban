using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{
    [Serializable]
    public class Day
    {
        public ContractConst.DAYS days { get; set; }
        public List<Place> place { get; set; }

        public Day()
        {            
            this.place = new List<Place>();
            this.days = new ContractConst.DAYS();
        }
    }
}
