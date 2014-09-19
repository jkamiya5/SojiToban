using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.CommonModule
{
    /// <summary>
    /// 同一清掃箇所判定
    /// </summary>
    public class SamePlaceJudge
    {
        /// <summary>
        /// この週に既に掃除したことのある箇所かを判定する
        /// </summary>
        /// <param name="member"></param>
        /// <param name="TargetPlace"></param>
        /// <returns></returns>
        public static Boolean Judge(Member member, int? TargetPlace)
        {
            if (member.day.Count() != 0)
            {
                foreach (var day in member.day)
                {
                    foreach (var place in day.place)
                    {
                        foreach (var p in place.value)
                        {
                            if (p == TargetPlace)
                            {
                                return false;
                            }
                        }
                    }
                }
            }else
            {
                return true;
            }
            return true;
        }
    }
}
