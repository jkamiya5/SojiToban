using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SojiToban.CommonModule;
using SojiToban.Dto;
using System.Collections.Generic;
using SojiToban.Service;
using System.Collections;
using System.Xml.Serialization;
using SojiTobanTest.TestDto;

namespace SojiTobanTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class LoccateOptionTest : MainService
    {


        /// <summary>
        /// 清掃可能か判定するメソッドのテスト
        /// </summary>
        [TestMethod]
        public void CheckCheckCleanable1()
        {

            LoccateOption target = new LoccateOption();
            Member member = new Member();
            //性別は「男」
            member.Gender = ContractConst.GENDER.男;
            //水曜日に「13」、金曜日に「21」を清掃した
            member.day = new List<Day>();

            //「水」のデータ作成
            Day day = new Day();
            Place place = new Place();
            day.days = ContractConst.DAYS.水;
            place.value.Enqueue(13);
            day.place.Add(place);
            member.day.Add(day);

            //「金曜」のデータ作成
            day = new Day();
            place = new Place();
            day.days = ContractConst.DAYS.金;
            place.value.Enqueue(21);
            day.place.Add(place);
            member.day.Add(day);

            //「13」という清掃箇所はすでにやったので「False」を返す
            int? targetPlace = 13;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));

            //「21」という清掃箇所はすでにやったので「False」を返す
            targetPlace = 21;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));

            //「22」という清掃箇所は清掃可能なので「True」を返す
            targetPlace = 22;
            Assert.IsTrue(target.CheckCleanable(member, targetPlace));

            //男の場合は、女子の清掃箇所「11, 17, 24, 28」を引数に取るとだと「FALSE」を返す。
            targetPlace = 11;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
            targetPlace = 17;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
            targetPlace = 24;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
            targetPlace = 28;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
        }


        /// <summary>
        /// 清掃可能か判定するメソッドのテスト
        /// </summary>
        [TestMethod]
        public void CheckCheckCleanable2()
        {

            LoccateOption target = new LoccateOption();
            Member member = new Member();
            //性別は「女」
            member.Gender = ContractConst.GENDER.女;
            //水曜日に「13」、金曜日に「21」を清掃した設定
            member.day = new List<Day>();

            //「水」のデータ作成
            Day day = new Day();
            Place place = new Place();
            day.days = ContractConst.DAYS.水;
            place.value.Enqueue(13);
            day.place.Add(place);
            member.day.Add(day);

            //「金曜」のデータ作成
            day = new Day();
            place = new Place();
            day.days = ContractConst.DAYS.金;
            place.value.Enqueue(21);
            day.place.Add(place);
            member.day.Add(day);

            //「13」という清掃箇所はすでにやったので「False」を返す
            int? targetPlace = 13;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));

            //「21」という清掃箇所はすでにやったので「False」を返す
            targetPlace = 21;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));

            //「22」という清掃箇所は清掃可能なので「True」を返す
            targetPlace = 22;
            Assert.IsTrue(target.CheckCleanable(member, targetPlace));


            //女の場合は、女子の清掃箇所「11, 17, 24, 28」を引数に取ると「True」を返す。
            targetPlace = 11;
            Assert.IsTrue(target.CheckCleanable(member, targetPlace));
            targetPlace = 17;
            Assert.IsTrue(target.CheckCleanable(member, targetPlace));
            targetPlace = 24;
            Assert.IsTrue(target.CheckCleanable(member, targetPlace));
            targetPlace = 28;
            Assert.IsTrue(target.CheckCleanable(member, targetPlace));

            //女の場合は、男子の清掃箇所「10, 12, 16, 23, 27」を引数に取ると「FALSE」を返す。
            targetPlace = 10;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
            targetPlace = 12;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
            targetPlace = 16;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
            targetPlace = 23;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
            targetPlace = 27;
            Assert.IsFalse(target.CheckCleanable(member, targetPlace));
        }


        /// <summary>
        /// 配置を行うメソッドのテスト
        /// </summary>
        [TestMethod]
        public void CheckMethod_AllocationFirstTime()
        {
            //清掃箇所をランダムに割り振った数字列作成
            DataOption dataOption = new DataOption();
            //ランダムに作成した掃除割り当て格納用変数作成
            RandamWeekMap RandamWeekMap = dataOption.CreateNumMap();
            //XMLフォーマットから復元する
            Queue<Member> Team = dataOption.DeSerializeTeamData();
            //テストしたいクラス
            LoccateOption target = new LoccateOption();

            //日毎にチームメンバーを配備
            foreach (var EachDay in RandamWeekMap.day)
            {
                foreach (Member member in Team)
                {
                    if (EachDay.place[0].value.Count > 0)
                    {
                        Day EachDayCopy = new Day();
                        EachDayCopy = EachDay.CloneDeep();
                        Member memberCopy = new Member();
                        memberCopy = member.CloneDeep();

                        Assert.IsTrue(target.AllocationFirstTime(EachDay, member));
                        Assert.IsTrue(target.AssignmentEachDay(EachDayCopy, memberCopy));

                        Assert.AreEqual(member.No, memberCopy.No);
                        Assert.AreEqual(member.Info, memberCopy.Info);
                        Assert.AreEqual(member.Name, memberCopy.Name);                        
                        Assert.AreEqual(member.Score, memberCopy.Score);
                        Assert.AreEqual(member.Gender, memberCopy.Gender);

                        for (int i = 0; i < member.day.Count; i++ )
                        {
                            for (int j = 0; j < member.day[i].place.Count; j++)
                            {
                                Assert.AreEqual(member.day[i].place[j].value.Peek(),
                                                memberCopy.day[i].place[j].value.Peek());
                            }
                        }                    
                    }else
                    {
                        Assert.IsFalse(target.AllocationFirstTime(EachDay, member));
                    }                    
                }
            }
            System.Diagnostics.Debug.Write("end");
        }
    }
}
