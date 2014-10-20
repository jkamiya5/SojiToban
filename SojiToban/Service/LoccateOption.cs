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
        public bool AssignmentEachDay(Day EachDay, Member member)
        {
            //カレントの曜日の清掃箇所ランダムリストを回す
            foreach (Place CleaningPart in EachDay.place)
            {
                //割り当てオブジェクト作成
                Day today = new Day();
                //曜日を設定
                today.days = EachDay.days;

                //清掃箇所のランダムキューに値が存在する間ループを回す
                if (CleaningPart.value.Count > 0)
                {
                    //先頭の清掃箇所を取得                    
                    int? randamPlaceValue = CleaningPart.value.Dequeue();
                    //清掃可能か判定する
                    bool IsCleanablePlace = CheckCleanable(member, randamPlaceValue);
                    
                    //清掃可能な箇所の場合
                    if (IsCleanablePlace)
                    {
                        //清掃可能な箇所リストに追加する
                        member = this.AddCleanablePlaceList(member, today, randamPlaceValue);
                    }
                    else
                    {
                        //清掃不可能な箇所の場合
                        //清掃すべき箇所の待ちキューに値を追加する
                        CleaningPart.value.Enqueue(randamPlaceValue);
                    }
                }
                else
                {
                    //清掃すべき箇所がなくなった場合
                    return false;
                }
            }
            //次のメンバーへ
            return true;
        }


        /// <summary>
        /// 清掃可能な箇所一覧に追加する
        /// </summary>
        /// <param name="member"></param>
        /// <param name="today"></param>
        /// <param name="randamPlaceValue"></param>
        private Member AddCleanablePlaceList(Member member, Day today, int? randamPlaceValue)
        {
            //担当箇所オブジェクト作成
            Place ResponsiblePlace = new Place();
            //同日内清掃箇所リストに追加
            ResponsiblePlace.value.Enqueue(randamPlaceValue);
            //カレントの曜日の清掃箇所を決定
            today.place.Add(ResponsiblePlace);
            //得点を足しこむ
            member.Score += randamPlaceValue == null ? 0: ContractConst.COEFFICIENT[(int)randamPlaceValue];
            //備考の内容を設定
            member.Info += today.days.ToString() + randamPlaceValue.ToString() + " ";
            //今日の割り当てを決める
            member.day.Add(today);
            //値を返す
            return member;
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
                    if (CheckCleanable(member, randamPlaceValue))
                    {
                        //担当箇所オブジェクト作成
                        Place ResponsiblePlace = new Place();
                        //同日内清掃箇所リストに追加
                        ResponsiblePlace.value.Enqueue(randamPlaceValue);
                        //カレントの曜日の清掃箇所を決定
                        today.place.Add(ResponsiblePlace);
                        
                        if (randamPlaceValue == null)
                        {
                            //清掃箇所が取得できない場合は「0」を加算
                            member.Score += 0;
                        }
                        else
                        {
                            //清掃箇所の難儀度に応じた得点を足しこむ
                            member.Score += ContractConst.COEFFICIENT[(int)randamPlaceValue];
                        }
                        member.day.Add(today);
                        //備考欄に記載する情報を作成
                        member.Info += today.days.ToString() + randamPlaceValue.ToString() + " ";
                    }
                    else
                    {
                        //清掃可能な箇所ではないため、再度、清掃箇所待ちキューに格納する
                        CleaningPart.value.Enqueue(randamPlaceValue);
                    }
                }
                else
                {
                    //割り振り失敗で終了
                    return false;
                }
            }
            //割り振り成功で終了
            return true;
        }


        /// <summary>
        /// 清掃可能か判定する
        /// </summary>
        /// <param name="member"></param>
        /// <param name="TargetPlace"></param>
        /// <param name="dAYS"></param>
        /// <returns></returns>
        public bool CheckCleanable(Member member, int? TargetPlace)
        {
            //男女毎清掃箇所判定
            if (!GenderAllocationJudge.Judge(member.Gender, TargetPlace))
            {
                return false;
            }
            //同一清掃箇所判定
            if (!SamePlaceJudge.Judge(member, TargetPlace))
            {
                return false;
            }
            return true;
        }
    }
}
