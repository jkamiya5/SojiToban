using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;
using System.Collections;

namespace SojiToban.CommonModule
{
    public static class Util
    {
        /// <summary>
        /// ディープコピーを作成する。
        /// クローンするクラスには SerializableAttribute 属性、
        /// 不要なフィールドは NonSerializedAttribute 属性をつける。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T CloneDeep<T>(this T target)
        {
            object clone = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, target);
                stream.Position = 0;
                clone = formatter.Deserialize(stream);
            }
            return (T)clone;
        }

        /// <summary>
        /// 分散を返す拡張メソッド
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Target"></param>
        /// <returns></returns>
        public static double PopulationStandardDeviationT<T>(this IEnumerable<T> Target)
        {
            List<double> Ret = new List<double>();
            foreach(var v in Target)
            {
                Ret.Add(Convert.ToDouble(v));
            }
            double StandardDeviation = Ret.PopulationStandardDeviation();
            return StandardDeviation;
        }
    }
}
