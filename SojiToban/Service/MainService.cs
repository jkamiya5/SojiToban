using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using MathNet.Numerics.Statistics;
using SojiToban.CommonModule;
using System.Security.Cryptography;

namespace SojiToban.Service
{


    /// <summary>
    /// 
    /// </summary>
    class MainService
    {
        internal void MainProc(Queue<Member> Team)
        {
            //清掃箇所をランダムに割り振った数字列作成
            RandamWeekMap RandamWeekMap = CreateNumMap();

            //曜日毎に割り振りを行う
            foreach (Day EachDay in RandamWeekMap.day)
            {
                Member ret = new Member();
                //メンバー全員に対して割り振り処理を行う
                foreach (Member member in Team)
                {
                    //個人割り振り初回時
                    if (member.day.Count() == 0)
                    {
                        ret = AllocationFirstTime(EachDay, member);

                    }
                    else
                    {
                        ret = Allocation(EachDay, member);

                    }
                    System.Diagnostics.Debug.WriteLine(member);
                }                
                //メンバーのランダムソートを行う
                IEnumerable<Member> query = Team.OrderBy(person => person.score);
                foreach (Member member in query)
                {
                    Console.WriteLine("{0} - {1}", member.Name, member.score);
                    Team.Dequeue();
                    Team.Enqueue(member);
                }
                System.Diagnostics.Debug.WriteLine(EachDay);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EachDay"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        private Member Allocation(Day EachDay, Member member)
        {
            //個人割り振り2回目以降
            System.Diagnostics.Debug.WriteLine("//個人割り振り2回目以降");
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
                    }
                }
                member.day.Add(today);
            }            
            return member;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EachDay"></param>
        /// <param name="member"></param>
        private Member AllocationFirstTime(Day EachDay, Member member)
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
                        }
                    }
                }
            }
            member.day.Add(today);
            return member;
        }


        /// <summary>
        /// 清掃可否判定を行う
        /// </summary>
        /// <param name="member"></param>
        /// <param name="randamPlaceValue"></param>
        /// <returns></returns>
        private bool CheckCleanable(Member member, int randamPlaceValue, ContractConst.DAYS days)
        {
            //男女毎清掃箇所判定
            if (!GenderAllocationJudge.Judge(member.Gender, randamPlaceValue))
            {
                return false;
            }
            //同日割り当て判定
            if (!SameDayAssignmentJudge.Judge(member, days, randamPlaceValue))
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="randamPlaceValue"></param>
        /// <returns></returns>
        private bool CheckCleanable(int randamPlaceValue)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="RandamWeekMap"></param>
        /// <param name="pSchedule"></param>
        /// <param name="tSchedule"></param>
        /// <returns></returns>
        //private TeamSchedule Assign(List<Queue<int>> RandamWeekMap)
        //{
        //    System.Random rng = new System.Random();
        //    List<int> Works = new List<int>();
        //    Schedule tSchedule = new Schedule();            

        //    for (int i = 0; i < RandamWeekMap.Count(); i++ )
        //    {
        //        while (RandamWeekMap[i].Count > 0)
        //        {
        //            PersonalSchedule pSchedule = new PersonalSchedule();
        //            pSchedule.dayIndex.Add(i);
        //            int N = RandamWeekMap[i].Dequeue();
        //            pSchedule.placeIndex.Enqueue(N);
        //            pSchedule.score += ContractConst.COEFFICIENT[N];
        //            tSchedule.pSchedule.Add(pSchedule);                    
        //        }
        //        System.Diagnostics.Debug.WriteLine(tSchedule);
        //    }          
        //    return tSchedule;
        //}



        /// <summary>
        /// ランダムな清掃箇所振り分けマップを作成
        /// </summary>
        /// <returns></returns>
        private RandamWeekMap CreateNumMap()
        {
            Day day = new Day();
            RandamWeekMap RandamWeekMap = new RandamWeekMap();
            for (int i = 0; i < ContractConst.WEEK.Count() - 1; i++)
            {
                int[] obj = (int[])ContractConst.WEEK[i];
                day = DayLoccation(obj);
                RandamWeekMap.day.Add(day);

            }
            int j = 0;
            foreach (ContractConst.DAYS v in Enum.GetValues(typeof(ContractConst.DAYS)))
            {
                RandamWeekMap.day[j].days = v;
                j++;
            }
            System.Diagnostics.Debug.WriteLine(RandamWeekMap);
            return RandamWeekMap;
        }


        /// <summary>
        /// 日ごとに清掃箇所のランダム配列を作成
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Day DayLoccation(int[] p)
        {
            Day day = new Day();
            Place place = new Place();
            while (place.value.Count() < p.Count())
            {
                System.Random rng = new System.Random();
                int k = rng.Next(p.Count());
                int num = p[k];
                if (!place.value.Contains(num))
                {
                    place.value.Enqueue(p[k]);
                }
            }
            day.place.Add(place);
            return day;
        }
    }

}
