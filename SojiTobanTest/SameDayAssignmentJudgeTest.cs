using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SojiToban.CommonModule;
using SojiToban.Dto;

namespace SojiTobanTest
{
    [TestClass]
    public class SameDayAssignmentJudgeTest
    {
        [TestMethod]
        public void CheckSameDayAssignmentJudge()
        {
            Member member = new Member();            
            ContractConst.DAYS? targetDay = ContractConst.DAYS.月;
            Assert.IsTrue(SameDayAssignmentJudge.Judge(member, targetDay));
        }
    }
}
