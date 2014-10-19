using SojiToban.CommonModule;
using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Service
{


    /// <summary>
    /// 配置に関するクラス
    /// </summary>
    public class LoccateOption
    {
        /// <summary>
        /// 配置を行う
        /// </summary>
        /// <param name="EachDay"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool Allocation(Day EachDay, Member member)
        {
            //カレントの曜日の清掃箇所ランダムリストを回す
            foreach (var CleaningPart in EachDay.place)
            {
                //割り当てオブジェクト作成
                Day today = new Day();
                //曜日を設定
                today.days = EachDay.days;
                if (EachDay.days == ContractConst.DAYS.金)
                {
                    System.Diagnostics.Debug.WriteLine("");
                }

                //清掃箇所のランダムキューに値が存在する間ループを回す
                if (CleaningPart.value.Count > 0)
                {
                    //先頭の清掃箇所を取得                    
                    int? randamPlaceValue = CleaningPart.value.Dequeue();
                    //清掃可否判定を行う
                    if (CheckCleanable(member, randamPlaceValue, today.days))
                    {
                        //担当箇所オブジェクト作成
                        Place ResponsiblePlace = new Place();
                        //同日内清掃箇所リストに追加
                        ResponsiblePlace.value.Enqueue(randamPlaceValue);
                        //カレントの曜日の清掃箇所を決定
                        today.place.Add(ResponsiblePlace);
                        //得点を足しこむ
                        if (randamPlaceValue == null)
                        {
                            member.Score += 0;
                        }
                        else
                        {
                            member.Score += ContractConst.COEFFICIENT[(int)randamPlaceValue];
                        }
                        member.day.Add(today);
                        member.Info += today.days.ToString() + randamPlaceValue.ToString() + " ";
                    }
                    else
                    {
                        CleaningPart.value.Enqueue(randamPlaceValue);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 初回配置を行う
        /// </summary>
        /// <param name="EachDay"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool AllocationFirstTime(Day EachDay, Member member)
        {
            //割り当てオブジェクト作成
            Day today = new Day();
            //曜日を設定
            today.days = EachDay.days;

            //カレントの曜日の清掃箇所ランダムリストを回す
            foreach (var CleaningPart in EachDay.place)
            {
                //清掃箇所のランダムキューに値が存在する間ループを回す
                if (CleaningPart.value.Count > 0)
                {
                    //先頭の清掃箇所を取得
                    int? randamPlaceValue = CleaningPart.value.Dequeue();
                    //清掃可否判定を行う
                    if (CheckCleanable(member, randamPlaceValue, today.days))
                    {
                        //担当箇所オブジェクト作成
                        Place ResponsiblePlace = new Place();
                        //同日内清掃箇所リストに追加
                        ResponsiblePlace.value.Enqueue(randamPlaceValue);
                        //カレントの曜日の清掃箇所を決定
                        today.place.Add(ResponsiblePlace);
                        //得点を足しこむ
                        if (randamPlaceValue == null)
                        {
                            member.Score += 0;
                        }
                        else
                        {
                            member.Score += ContractConst.COEFFICIENT[(int)randamPlaceValue];
                        }
                        member.day.Add(today);
                        member.Info += today.days.ToString() + randamPlaceValue.ToString() + " ";
                    }
                    else
                    {
                        CleaningPart.value.Enqueue(randamPlaceValue);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 清掃可能か判定する
        /// </summary>
        /// <param name="member"></param>
        /// <param name="randamPlaceValue"></param>
        /// <param name="dAYS"></param>
        /// <returns></returns>
        public bool CheckCleanable(Member member, int? randamPlaceValue, ContractConst.DAYS dAYS)
        {
            //男女毎清掃箇所判定
            if (!GenderAllocationJudge.Judge(member.Gender, randamPlaceValue))
            {
                return false;
            }
            //同日割り当て判定
            //if (!SameDayAssignmentJudge.Judge(member, dAYS, randamPlaceValue))
            //{
            //    return false;
            //}
            //同一清掃箇所判定
            if (!SamePlaceJudge.Judge(member, randamPlaceValue))
            {
                return false;
            }
            return true;
        }
    }
}
