using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SojiToban.CommonModule;
using SojiToban.Dto;
using System.Collections.Generic;

namespace SojiTobanTest
{
    [TestClass]
    public class SamePlaceJudgeTest
    {
        [TestMethod]
        public void CheckSamePlaceJudge1()
        {
            //「1」という清掃箇所を掃除したとするデータを作成           
            Place place = new Place();
            place.value.Enqueue(1);
            Day day = new Day();
            day.place.Add(place);
            Member member = new Member();
            member.day = new List<Day>();
            member.day.Add(day);

            //「今回新たに1という清掃箇所を担当した」場合
            int? TargetPlace = 1;
            //すでにやったことのある箇所なので「False」となる
            Assert.IsFalse(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たに2という清掃箇所を担当した」場合
            TargetPlace = 2;
            //まだやったことのない箇所なので「True」となる
            Assert.IsTrue(SamePlaceJudge.Judge(member, TargetPlace));

        }

        [TestMethod]
        public void CheckSamePlaceJudge2()
        {
            //同日に「1,4」という清掃箇所を掃除したとするデータを作成           
            Place place = new Place();
            place.value.Enqueue(1);
            place.value.Enqueue(4);
            Day day = new Day();
            day.place.Add(place);
            Member member = new Member();
            member.day = new List<Day>();
            member.day.Add(day);

            //「今回新たに1という清掃箇所を担当した」場合
            int? TargetPlace = 1;
            //すでにやったことのある箇所なので「False」となる
            Assert.IsFalse(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たに2という清掃箇所を担当した」場合
            TargetPlace = 2;
            //まだやったことのない箇所なので「True」となる
            Assert.IsTrue(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たに4という清掃箇所を担当した」場合
            TargetPlace = 4;
            //すでにやったことのある箇所なので「False」となる
            Assert.IsFalse(SamePlaceJudge.Judge(member, TargetPlace));

        }

        [TestMethod]
        public void CheckSamePlaceJudge3()
        {
            //同日に「3,12」という清掃箇所を掃除したとするデータを作成           
            Place place = new Place();
            place.value.Enqueue(3);
            place.value.Enqueue(12);
            Day day = new Day();
            day.place.Add(place);
            Member member = new Member();
            member.day = new List<Day>();
            member.day.Add(day);

            //「今回新たに1という清掃箇所を担当した」場合
            int? TargetPlace = 1;
            //まだやったことのない箇所なので「True」となる
            Assert.IsTrue(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たに2という清掃箇所を担当した」場合
            TargetPlace = 2;
            //まだやったことのない箇所なので「True」となる
            Assert.IsTrue(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たにnullという清掃箇所を担当した」場合
            TargetPlace = null;
            //異常値を引数に取ったため「False」となる
            Assert.IsFalse(SamePlaceJudge.Judge(member, TargetPlace));

        }


        [TestMethod]
        public void CheckSamePlaceJudge4()
        {
            //--------------------------------------
            //テストデータ作成
            //--------------------------------------

            //月曜日に「7」、木曜日に「13」という清掃箇所を掃除したとするデータを作成
            Member member = new Member();
            member.day = new List<Day>();

            //「月」のデータ作成
            Day day = new Day();
            Place place = new Place();
            day.days = ContractConst.DAYS.月;            
            place.value.Enqueue(7);
            day.place.Add(place);
            member.day.Add(day);


            //「木」のデータ作成
            day = new Day();
            place = new Place();
            day.days = ContractConst.DAYS.木;
            place.value.Enqueue(13);
            day.place.Add(place);
            member.day.Add(day);


            //--------------------------------------
            //テスト実施
            //--------------------------------------

            //「今回新たに1という清掃箇所を担当した」場合
            int? TargetPlace = 1;
            //まだやったことのない箇所なので「True」となる
            Assert.IsTrue(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たに2という清掃箇所を担当した」場合
            TargetPlace = 2;
            //まだやったことのない箇所なので「True」となる
            Assert.IsTrue(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たに7という清掃箇所を担当した」場合
            TargetPlace = 7;
            //すでにやったことのある箇所なので「False」となる
            Assert.IsFalse(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たに13という清掃箇所を担当した」場合
            TargetPlace = 13;
            //すでにやったことのある箇所なので「False」となる
            Assert.IsFalse(SamePlaceJudge.Judge(member, TargetPlace));

            //「今回新たにnullという清掃箇所を担当した」場合
            TargetPlace = null;
            //異常値を引数に取ったため「False」となる
            Assert.IsFalse(SamePlaceJudge.Judge(member, TargetPlace));

        }
    }
}
