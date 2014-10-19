using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SojiToban.CommonModule;
using SojiToban.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SojiTobanTest
{
    [TestClass]
    public class GenderAllocationJudgeTest
    {
        [TestMethod]
        public void CheckManLoccateTest() {

            
            //-------------------------------------//
            // 正常系のテスト
            //-------------------------------------//
            
            //男の場合は、女子の清掃箇所「11, 17, 24, 28」を引数に取るとだと「FALSE」を返す。
            //それ以外は「TRUE」。
            ContractConst.GENDER? Gender = ContractConst.GENDER.男;
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 0));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 1));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 2));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 3));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 4));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 5));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 6));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 7));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 8));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 9));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 10));            
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 12));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 13));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 14));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 15));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 16));            
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 18));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 19));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 20));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 21));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 22));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 23));            
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 25));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 26));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 27));            


            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 11));
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 17));
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 24));
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 28));

            //-------------------------------------//
            // 異常系のテスト
            //-------------------------------------//
            //NULLが入った場合には「FALSE」を返す。            
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, null));
            //負数が入った場合には「FALSE」を返す。
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, -1));

        }

        [TestMethod]
        public void CheckWoManLoccateTest()
        {
            //-------------------------------------//
            // 正常系のテスト
            //-------------------------------------//

            //女の場合は、男子の清掃箇所「10, 12, 16, 23, 27」を引数に取るとだと「FALSE」を返す。
            //それ以外は「TRUE」。
            ContractConst.GENDER? Gender = ContractConst.GENDER.女;
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 0));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 1));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 2));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 3));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 4));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 5));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 6));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 7));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 8));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 9));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 11));            
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 13));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 14));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 15));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 17));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 18));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 19));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 20));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 21));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 22));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 24));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 25));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 26));
            Assert.IsTrue(GenderAllocationJudge.Judge(Gender, 28));

            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 10));
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 12));
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 16));
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 23));
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 27));

            //-------------------------------------//
            // 異常系のテスト
            //-------------------------------------//
            //NULLが入った場合には「FALSE」を返す。            
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, null));
            //負数が入った場合には「FALSE」を返す。
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, -1));
        }

        [TestMethod]
        public void CheckAbnormalSystemTest()
        {
            ContractConst.GENDER? Gender = ContractConst.GENDER.男;
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 29));
            Gender = ContractConst.GENDER.女;
            Assert.IsFalse(GenderAllocationJudge.Judge(Gender, 29));
        }
    }
}
