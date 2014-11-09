using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SojiToban.Dto;

namespace SojiToban.CommonModule
{
    /// <summary>
    /// 男女毎清掃箇所判定
    /// </summary>
    public class GenderAllocationJudge
    {

        /// <summary>
        /// 男女毎に、指定した箇所の清掃が可能か判定する
        /// </summary>
        /// <param name="Gender"></param>
        /// <param name="TargetPlace"></param>
        /// <returns></returns>
        public static Boolean Judge(ContractConst.GENDER? Gender, int? TargetPlace)
        {
            //NULLや負数、清掃箇所のインデックスを外れる値が入った場合には「False」を返し処理終了
            if (TargetPlace == null || TargetPlace < 0 || TargetPlace > ContractConst.PLACE_COUNT)
            {
                return false;
            }
            if (Gender == ContractConst.GENDER.男)
            {
                for (int i = 0; i < ContractConst.WOMAN_LOCATE.Count(); i++)
                {
                    if (TargetPlace == ContractConst.WOMAN_LOCATE[i])
                    {
                        return false;
                    }
                }
            }
            else if (Gender == ContractConst.GENDER.女)
            {
                for (int i = 0; i < ContractConst.MAN_LOCATE.Count(); i++)
                {
                    if (TargetPlace == ContractConst.MAN_LOCATE[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
