using App;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test
{
    [TestClass]
    public class RomanNUmberTest
    {
        Dictionary<int, string> _digitValues = new Dictionary<int, string>() { };

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
        public void _CheckSymbolsTest()
        {
            Type? rnType = typeof(RomanNumberFactory);
            MethodInfo? m1Info = rnType.GetMethod("_CheckSymbols",
            BindingFlags.NonPublic | BindingFlags.Static);

            // Assert Not Throws
            m1Info?.Invoke(null, ["IX"]);

            var ex = Assert.ThrowsException<TargetInvocationException>(
            () => m1Info?.Invoke(null, ["IW"]),
            $"Parse 'IW' must throw FormatException"
                );

            Assert.IsInstanceOfType<FormatException>(
             ex.InnerException,
             $"FormatException from InnerException");

        }
        [TestMethod]
        public void _CheckFormatTest()
        {
            Type? rnType = typeof(RomanNumberFactory);
            MethodInfo? m1Info = rnType.GetMethod("_CheckFormat",
            BindingFlags.NonPublic | BindingFlags.Static);

            // Assert Not Throws
            m1Info?.Invoke(null, ["IX"]);

            var ex = Assert.ThrowsException<TargetInvocationException>(
            () => m1Info?.Invoke(null, ["IIX"]),
            $"_CheckFormat 'IIX' must throw FormatException"
                );

            Assert.IsInstanceOfType<FormatException>(
             ex.InnerException,
             $"_CheckFormat:FormatException from InnerException");

        }
        [TestMethod]
        public void _CheckValidityTest()
        {
            Type? rnType = typeof(RomanNumberFactory);
            MethodInfo? m1Info = rnType.GetMethod("_CheckValidity",
            BindingFlags.NonPublic | BindingFlags.Static);

            // Assert Not Throws
            m1Info?.Invoke(null, ["IX"]);

            string[] testCases = ["IXIX", "IXX", "IVIV", "XCC", "IXIV", "XCXL","CMCD"];
            foreach ( var testCase in testCases )
            {

                var ex = Assert.ThrowsException<TargetInvocationException>(
                () => m1Info?.Invoke(null, [testCase]),
                $"_CheckValidity '{testCase}' must throw FormatException"
                    );

                Assert.IsInstanceOfType<FormatException>(
                 ex.InnerException,
                 $"_CheckValidity:FormatException from InnerException");
            }

        }

        [TestMethod]
        public void ParseTest()
        {
            var testCases = new Dictionary<string, int>()
            {
                { "N",      0 },
                { "I",      1 },
                { "II",     2 },
                { "III",    3 },
                { "IV",     4 },
                { "V",      5 },
                { "VI",     6 },
                { "VII",    7 },
                { "VIII",   8 },
                { "D",      500 },
                { "CM",     900 },
                { "M",      1000 },
                { "MC",     1100 },
                { "MCM",    1900 },
                { "MM",     2000 },
            };

            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumberFactory.Parse(testCase.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(testCase.Value
                    ,
                    rn.Value,
                    $"{testCase.Key} parsing failed. Expected {testCase.Value}, got {rn.Value}."
                );
            }

            //Dictionary<string, (char, int)[]> exTestCases = new()
            //{
            //    {"W", [('W', 0)] },
            //    {"Q", [('Q', 0)] },
            //    {"s", [('s', 0)] },
            //    {"Xd", [('d', 1)] },
            //    {"SWXF", [('S', 0), ('W', 1), ('F', 3)] },
            //    {"XXFX", [('F', 2)] },
            //    {"VVVFX", [('F', 3)] },
            //    {"IVF", [('F', 2)] },
            //};

            //foreach (var testCase in exTestCases)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNumber.Parse(testCase.Key),
            //        $"{nameof(FormatException)} Parse '{testCase.Key}' must throw");

            //    foreach (var (symbol, position) in testCase.Value)
            //    {
            //        Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
            //            $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
            //            $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'");
            //    }
            //}




            //Dictionary<string, object[]> exTestCases2 = new()
            //{
            //    { "IM",  ['I', 'M', 0] },
            //    { "XIM", ['I', 'M', 1] },
            //    { "IMX", ['I', 'M', 0] },
            //    { "XMD", ['X', 'M', 0] },
            //    { "XID", ['I', 'D', 1] },
            //    { "ID", ['I', 'D', 0] },
            //    { "XM", ['X', 'M', 0] },


            //};
            //foreach (var testCase in exTestCases2)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNumber.Parse(testCase.Key),
            //        $"Parse '{testCase.Key}' must throw FormatException"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains(
            //            $"Invalid order '{testCase.Value[0]}' before '{testCase.Value[1]}' in position {testCase.Value[2]}"
            //        ),
            //        "FormatException must contain data about mis-ordered symbols and its position"
            //        + $"testCase: '{testCase.Key}', ex.Message: '{ex.Message}'"
            //    );
            //}

            //    Dictionary<string, (char, int)[]> exTestCases3 = new()
            //{

            //    { "XVV", [('V', 2)] },
            //    { "LL", [('L', 1)] },
            //    { "LC", [('C', 1)] },
            //    { "VX", [('X', 1)] },
            //    { "MM", [('M', 1)] },

            //};

            //    foreach (var testCase in exTestCases3)
            //    {
            //        var ex = Assert.ThrowsException<FormatException>(
            //            () => RomanNumber.Parse(testCase.Key),
            //            $"{nameof(FormatException)} Parse '{testCase.Key}' must throw");

            //        foreach (var (symbol, position) in testCase.Value)
            //        {
            //            Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
            //                $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
            //                $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'");
            //        }
            //    }
            //string[] exTestCases4 =
            //{
            //    "IXC", "IIX", "VIX",
            //    "IIXC", "IIIX", "VIIX",
            //    "VIXC", "IVIX", "CVIIX",
            //    "IXCC", "IXCM", "IXXC",
            //};
            //foreach (var testCase in exTestCases4)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNumber.Parse(testCase),
            //        $"Parse '{testCase}' must throw FormatException"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains(nameof(RomanNumber)) &&
            //        ex.Message.Contains(nameof(RomanNumber.Parse)) &&
            //        ex.Message.Contains($"invalid sequence: more than 1 less digit before '{testCase[^1]}'"),
            //        $"ex.Message must contain info about origin, cause and data. {ex.Message}");
            //}
            //string[] exTestCases5 =
            //{
            //    "IXIX", "IXX", "IXIV", 
            //    "XCXC", "CMM", "CMCD",
            //    "XCXL", "XCC", "XCCI"
            //};
            //foreach (var testCase in exTestCases5)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNumberFactory.Parse(testCase),
            //        $"Parse '{testCase}' must throw FormatException"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains(nameof(RomanNumber)) &&
            //        ex.Message.Contains(nameof(RomanNumberFactory.Parse)) &&
            //        ex.Message.Contains($"invalid sequence: more than 1 less digit before '{testCase[^1]}'"),
            //        $"ex.Message must contain info about origin, cause and data. {ex.Message}");
            //}
        }
        [TestMethod]
         public void DigitValueTest()
        {
            Dictionary<string, int> testCases = new()
            {
                {"N", 0 },
                {"I", 1 },
                {"V", 5 },
                {"X", 10 },
                {"L", 50 },
                {"C", 100 },
                {"D", 500 },
                {"M", 1000 },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    RomanNumberFactory.DigitValue(testCase.Key),
                    $"{testCase.Key} -> {testCase.Value}");
            }

            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                string invalidDigit = ((char)random.Next(256)).ToString();
                if (testCases.ContainsKey(invalidDigit))
                {
                    i--;
                    continue;
                }
                ArgumentException ex = Assert.ThrowsException<ArgumentException>(
                    () => RomanNumberFactory.DigitValue(invalidDigit),
                    $"ArgumentException expected for digit = '{invalidDigit}'"
                    );
                Assert.IsFalse(
                   string.IsNullOrEmpty(ex.Message),
                   "ArgumnetExceptionmust have a message"
                );
                Assert.IsTrue(
                   ex.Message.Contains($"'digit' has invalid value '{invalidDigit}'"),
                   "ArgumnetException must must contain a <'digit' has invalid value ''>"
                   );
                Assert.IsTrue(
                   ex.Message.Contains($"'digit'"),
                   "ArgumnetExceptionmust must contain a 'digit'"
                   );
                Assert.IsTrue(
                   ex.Message.Contains(nameof(RomanNumber)) &&
                   ex.Message.Contains(nameof(RomanNumberFactory.DigitValue)),
                   $"ArgumnetExceptionmust must contain '{nameof(RomanNumber)}' and  '{nameof(RomanNumberFactory.DigitValue)}'"
                   );
                var ex2 = Assert.ThrowsException<FormatException>(
                    () => RomanNumberFactory.Parse("W"),
                    "Invalid format"
                    );
                Assert.IsTrue(
                    ex2.Message.Contains("Invalid symbol 'W' in position 0"),
                    "FormatException must contain data about symbol and it's position"
                    );
            }
        }
        [TestMethod]
        public void ToStringTest()
        {
            Dictionary<int, string> testCases = new Dictionary<int, string>()
            {
                { 1, "I" },
                { 2, "II"},
                { 3343, "MMMCCCXLIII" },
                { 4, "IV" },
                { 44, "XLIV" },
                { 9, "IX" },
                { 90, "XC" },
                { 1400, "MCD" },
                { 900, "CM" },
                { 999, "CMXCIX" },
                { 990, "CMXC" },
                { 444, "CDXLIV" },

            };
            _digitValues.Keys.ToList().ForEach(k => testCases.Add(k, _digitValues[k]));
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    new RomanNumber(testCase.Key).ToString(),
                    testCase.Value,
                    $"ToString({testCase.Key})--> {testCase.Value}");
            }
        }
        //[TestMethod]
        //public void PlusTest()
        //{
        //    RomanNumber rn1 = new(1);
        //    RomanNumber rn2 = new(2);
        //    RomanNumber rn3 = rn1.Plus(rn2);
        //    Assert.IsNotNull(rn3);
        //    Assert.IsInstanceOfType(rn3, typeof(RomanNumber),
        //        "Plus result must have RomanNumber type");
        //    Assert.AreNotSame(rn3, rn1,
        //        "Plus result is new instance, neither (v)first, not second arg");
        //    Assert.AreNotSame(rn3, rn2,
        //        "Plus result is new instance, neither first, not (v)second arg");
        //    Assert.AreEqual(rn1.Value + rn2.Value, rn3.Value,
        //        "Plus arithmetic");
        //}

    }
}