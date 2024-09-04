using App;

namespace Test
{
    [TestClass]
    public class RomanNUmberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            Dictionary<String, int> testCases = new()
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
            Dictionary<String, int> testCases = new()
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