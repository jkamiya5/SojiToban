using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Dto
{
    // XMLファイルに保存するデータ
    [System.Xml.Serialization.XmlRoot("SojiPlaceClass")]
    public class SojiPlaceClass
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlElement("SojiPlace")]
        public System.Collections.Generic.List<SojiPlace> Items { get; set; }
    }
}
