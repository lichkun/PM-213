using App;
using System.Collections.Generic;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test
{
    [TestClass]
    public class RomanNUmberTest
    {
        Dictionary<int, string> _digitValues = new Dictionary<int, string>() { };
            

        [TestMethod]
        public void ParseTest()
        {
            Dictionary<string, int> testCases = new()
            {
                { "N",    0 },
                { "I",    1 },
                { "II",   2 },
                { "III",  3 },
                { "IV",   4 },
                { "VI",   6 },
                { "VII",  7 },
                { "VIII", 8 },
                { "IX",   9 },
                { "D",    500 },
                { "M",    1000 },
                { "CM",   900 },
                { "MC",   1100 },
                { "MCM",  1900 },
                { "MM",   2000 },

                 {"IIII", 4},
                 {"VIIII", 9},
                 {"XXXX", 40},
                 {"LXXXX", 90},
                 {"CCCC", 400},
                 {"DCCCC", 900},
            };
            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumber.Parse(testCase.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(
                    testCase.Value,
                    rn.Value,
                    $"{testCase.Key} -> {testCase.Value}"
                );
            }
            Dictionary<string, (char,int)[]> testCases2 = new()
            {
                {"W", [('W', 0)] },
                {"Q", [('Q', 0)] },
                {"s", [('s', 0)] },
                {"Xd", [('d', 1)] },
                {"SWXF", [('S', 0), ('W', 1), ('F', 3)] },
                {"XXFX", [('F', 2)] },
                {"VVVFX", [('F', 3)] },
                {"IVF", [('F', 2)] },
            };
            foreach (var testCase in testCases2)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"{nameof(FormatException)} Parse '{testCase.Key}' must throw");

                foreach (var (symbol, position) in testCase.Value)
                {
                    Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
                        $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
                        $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'");
                }
            }
            Dictionary<string, object[]> exTestCases2 = new()
                {
                { "IM",  ['I', 'M', 0] },
                { "XIM", ['I', 'M', 1] },
                { "IMX", ['I', 'M', 0] },
                { "XMD", ['X', 'M', 0] },
                { "XID", ['I', 'D', 1] },
                { "ID", ['I', 'D', 0] },
                { "XM", ['X', 'M', 0] },


                };
            foreach (var testCase in exTestCases2)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"Parse '{testCase.Key}' must throw FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains(
                        $"Invalid order '{testCase.Value[0]}' before '{testCase.Value[1]}' in position {testCase.Value[2]}"
                    ),
                    "FormatException must contain data about mis-ordered symbols and its position"
                    + $"testCase: '{testCase.Key}', ex.Message: '{ex.Message}'"
                );
            }

            Dictionary<string, (char, int)[]> exTestCases3 = new()
            {

                { "XVV", [('V', 2)] },
                { "LL", [('L', 1)] },
                { "LC", [('C', 1)] },
                { "VX", [('X', 1)] },
                { "MM", [('M', 1)] },

            };

            foreach (var testCase in exTestCases3)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"{nameof(FormatException)} Parse '{testCase.Key}' must throw");

                foreach (var (symbol, position) in testCase.Value)
                {
                    Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
                        $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
                        $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'");
                }
            }

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
                    RomanNumber.DigitValue(testCase.Key),
                    $"{testCase.Key} -> {testCase.Value}");
            }

            Random random = new Random();
            for(int i = 0; i < 100; i++)
            {
                string invalidDigit = ((char) random.Next(256)).ToString();
                if (testCases.ContainsKey(invalidDigit))
                {
                    i--;
                    continue;
                }
                ArgumentException ex = Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.DigitValue(invalidDigit),
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
                   ex.Message.Contains(nameof(RomanNumber.DigitValue)),
                   $"ArgumnetExceptionmust must contain '{nameof(RomanNumber)}' and  '{nameof(RomanNumber.DigitValue)}'"
                   );
                var ex2 = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse("W"),
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
            _digitValues.Keys.ToList().ForEach(k => testCases.Add( k, _digitValues[k]));
            foreach (var testCase in testCases)
            {
                Assert.AreEqual( 
                    new RomanNumber(testCase.Key).ToString(),
                    testCase.Value,
                    $"ToString({testCase.Key})--> {testCase.Value}" );
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseTest_InvalidNumber()
        {
            var digits = new string[] { "", "ABC", "IIII", "MMMDCCCCLXXXVIII" };
            foreach(var dig in digits)
            {
                RomanNumber.Parse(dig);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseTest_NullInput()
        {
            RomanNumber.Parse(null);
        }
    }
}