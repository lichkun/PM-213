using App;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test
{
    [TestClass]
    public class RomanNUmberTest
    {
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
            for(int i = 0;i<100; i++)
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
               ex.Message.Contains($"'digit' has invalid value ''"),
               "ArgumnetExceptionmust must contain a <'digit' has invalid value ''>"
               );
            Assert.IsTrue(
               ex.Message.Contains($"'digit'"),
               "ArgumnetExceptionmust must contain a 'digit'"
               );
            Assert.IsTrue(
               ex.Message.Contains(nameof(RomanNumber))&&
               ex.Message.Contains(nameof(RomanNumber.DigitValue)),
               $"ArgumnetExceptionmust must contain '{nameof(RomanNumber)}' and  '{nameof(RomanNumber.DigitValue)}'"
               );
                var ex2 = Assert.ThrowsException<FormatException>(
                    ()=> RomanNumber.Parse("W"),
                    "Invalid format"
                    );
                Assert.IsTrue(
                    ex2.Message.Contains("Invalid symbol 'W' in position 0"),
                    "FormatException must contain data about symbol and it's position"
                    );
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