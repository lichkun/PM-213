﻿using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class RomanNUmberMathTest
    {
        [TestMethod]
        public void PlusTest()
        {
            RomanNumber 
                rn1=new(1), 
                rn2=new(2),
                rn3=new(3);

            Assert.AreEqual(
                6,
                RomanNumberMath.Plus(rn1, rn2, rn3).Value
                );
            Assert.AreEqual(
                6,
                RomanNumberMath.Plus([rn1, rn2, rn3]).Value
                ); 
        }
    }
}
