using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.CommonModule
{
    /// <summary>
    /// 同日割り当て判定
    /// </summary>
    public class SameDayAssignmentJudge
    {
        /// <summary>
        /// 同日に複数清掃箇所の割り当てがあるかを判定する
        /// </summary>
        /// <param name="member"></param>
        /// <param name="targetDay"></param>
        /// <returns></returns>
        public static Boolean Judge(Member member, ContractConst.DAYS? targetDay, int? i)
        {
            if (member.day.Count() != 0)
            {
                foreach (var d in member.day)
                {
                    if (d.days == targetDay)
                    {
                        return false;
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
