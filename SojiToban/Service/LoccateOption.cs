using SojiToban.CommonModule;
using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Service
{
    public class LoccateOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EachDay"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static Member Allocation(Day EachDay, Member member)
        {                        
            //カレントの曜日の清掃箇所ランダムリストを回す
            foreach (var CleaningPart in EachDay.place)
            {
                //割り当てオブジェクト作成
                Day today = new Day();
                //曜日を設定
                today.days = EachDay.days;

                //清掃箇所のランダムキューに値が存在する間ループを回す
                if (CleaningPart.value.Count > 0)
                {
                    //先頭の清掃箇所を取得
                    int randamPlaceValue = CleaningPart.value.Dequeue();
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
                        member.score += ContractConst.COEFFICIENT[randamPlaceValue];
                        member.day.Add(today);
                    }
                    else
                    {

                    }
                }
            }
            return member;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EachDay"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static dto.Member AllocationFirstTime(dto.Day EachDay, dto.Member member)
        {
            //割り当てオブジェクト作成
            Day today = new Day();
            //曜日を設定
            today.days = EachDay.days;

            //カレントの曜日の清掃箇所ランダムリストを回す
            foreach (var CleaningPart in EachDay.place)
            {
                //個人割り振り初回時
                if (member.day.Count == 0)
                {
                    //清掃箇所のランダムキューに値が存在する間ループを回す
                    if (CleaningPart.value.Count > 0)
                    {
                        //先頭の清掃箇所を取得
                        int randamPlaceValue = CleaningPart.value.Dequeue();
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
                            member.score += ContractConst.COEFFICIENT[randamPlaceValue];
                            member.day.Add(today);
                        }
                    }
                }
            }
            return member;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="randamPlaceValue"></param>
        /// <param name="dAYS"></param>
        /// <returns></returns>
        private static bool CheckCleanable(Member member, int randamPlaceValue, ContractConst.DAYS dAYS)
        {
            //男女毎清掃箇所判定
            if (!GenderAllocationJudge.Judge(member.Gender, randamPlaceValue))
            {
                return false;
            }
            //同日割り当て判定
            if (!SameDayAssignmentJudge.Judge(member, dAYS, randamPlaceValue))
            {
                return false;
            }
            //同一清掃箇所判定
            if (!SamePlaceJudge.Judge(member, randamPlaceValue))
            {
                return false;
            }
            return true;
        }
    }
}
