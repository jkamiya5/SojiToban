using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SojiToban.CommonModule;
using SojiToban.Dto;
using System.Collections.Generic;
using SojiToban.Service;

namespace SojiTobanTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class LoccateOptionTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckCheckCleanable1()
        {

            LoccateOption target = new LoccateOption();
            Member member = new Member();
            int? randamPlaceValue = 0;
            ContractConst.DAYS dAYS = ContractConst.DAYS.月;
            Assert.IsFalse(target.CheckCleanable(member, randamPlaceValue, dAYS));


        }
    }
}
