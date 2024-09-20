using App;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace Test
{
    [TestClass]
    public class RomanNUmberTest
    {
        public static Dictionary<int, string> _digitValues = new Dictionary<int, string>() { };

        [TestMethod]
        public void ConstructorTest()
        {
            var rn = new RomanNumber("IX");
            Assert.IsNotNull(rn);

            rn = new RomanNumber(3);
            Assert.IsNotNull(rn);
        }
        [TestMethod]
        public void ConvertTest()
        {
            var rn = new RomanNumber("IX");
            Assert.IsInstanceOfType<Int32>(rn.ToInt());
            Assert.IsInstanceOfType<UInt32>(rn.ToUnsignedInt());
            Assert.IsInstanceOfType<Int16>(rn.ToShort());
            Assert.IsInstanceOfType<UInt16>(rn.ToUnsignedShort());
            Assert.IsInstanceOfType<Single>(rn.ToFloat());
            Assert.IsInstanceOfType<Double>(rn.ToDouble());
        }
        
        [TestMethod]
        public void ToStringTest()
        {
            Dictionary<int, string> testCases = new()
            {
                { 2, "II" },
                { 3343, "MMMCCCXLIII" },
                { 4, "IV" },
                { 44, "XLIV" },
                { 9, "IX" },
                { 90, "XC" },
                { 1400, "MCD" },
                { 999, "CMXCIX" },
                { 444, "CDXLIV" },
                { 990, "CMXC" }
            };

            foreach (var (k, v) in RomanNumberFactoryTest._digitValues) testCases.Add(k,v);

            foreach (var testCase in testCases)
                Assert.AreEqual(
                    testCase.Value,
                    new RomanNumber(testCase.Key).ToString(),
                    $"ToString({testCase.Key}) --> {testCase.Value}"
                );
        }
    }
}


