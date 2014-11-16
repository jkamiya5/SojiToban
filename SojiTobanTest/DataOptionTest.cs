using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SojiToban.CommonModule;
using SojiToban.Dto;
using SojiToban.Service;
using System.Collections.Generic;

namespace SojiTobanTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class DataOptionTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateNumMap_ProcessingTimeTest()
        {
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //ストップウォッチを開始する
            sw.Start();
            //計測対象メソッド
            DataOption dataOption = new DataOption();
            RandamWeekMap RandamWeekMap = dataOption.CreateNumMap();

            //ストップウォッチを止める
            sw.Stop();
            //結果を表示する
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 12824);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 12000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 11000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 10000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 9000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 8000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 7000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 6000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 5000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 4000);

            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 3000);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2900);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2800);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2750);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2600);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2590);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2580);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2570);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2560);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2550);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2540);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2530);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2520);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2510);

            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2500);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2490);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2480);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2470);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2460);
            //Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 2450);


            Console.WriteLine(sw.Elapsed);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]       
        public void CreateNumMap_VarianceValueTest()
        {
            DataOption dataOption = new DataOption();
            RandamWeekMap RandamWeekMap = dataOption.CreateNumMap();

            //標準偏差を調べる
            foreach (var obj in RandamWeekMap.day)
            {
                if (obj.days == ContractConst.DAYS.月)
                {
                    Assert.IsTrue(obj.place[0].value.Count == ContractConst.MONDAY.Length);
                    double StandardDeviation = obj.place[0].value.PopulationStandardDeviationT();
                    Assert.IsTrue(StandardDeviation < 3.4600);
                }
                if (obj.days == ContractConst.DAYS.火)
                {
                    Assert.IsTrue(obj.place[0].value.Count == ContractConst.TUESDAY.Length);
                    double StandardDeviation = obj.place[0].value.PopulationStandardDeviationT();
                    Assert.IsTrue(StandardDeviation < 7.7700);
                }
                if (obj.days == ContractConst.DAYS.水)
                {
                    Assert.IsTrue(obj.place[0].value.Count == ContractConst.WEDNESDAY.Length);
                    double StandardDeviation = obj.place[0].value.PopulationStandardDeviationT();
                    Assert.IsTrue(StandardDeviation < 3.4600);
                }
                if (obj.days == ContractConst.DAYS.木)
                {
                    Assert.IsTrue(obj.place[0].value.Count == ContractConst.THURSDAY.Length);
                    double StandardDeviation = obj.place[0].value.PopulationStandardDeviationT();
                    Assert.IsTrue(StandardDeviation < 3.7500);
                }
                if (obj.days == ContractConst.DAYS.金)
                {
                    Assert.IsTrue(obj.place[0].value.Count == ContractConst.FRIDAY.Length);
                    double StandardDeviation = obj.place[0].value.PopulationStandardDeviationT();
                    Assert.IsTrue(StandardDeviation < 8.2700);
                }
            }
        }


        [TestMethod]
        public void DayLoccation_Test()
        {
            DataOption dataOption = new DataOption();
            for (int i = 0; i < ContractConst.WEEK.Length - 1; i++)
            {
                int[] obj = (int[])ContractConst.WEEK[i];
                Day day = dataOption.DayLoccation(obj);             
            }
        }
    }
}
