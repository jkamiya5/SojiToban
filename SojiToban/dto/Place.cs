using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{
    [Serializable]
    public class Place
    {
        public Queue<int?> value { get; set; }
        public Place()
        {
            this.value = new Queue<int?>();
        }
    }
}
