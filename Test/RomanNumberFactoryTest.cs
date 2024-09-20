using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class RomanNumberFactoryTest
    {
        public static Dictionary<int, string> _digitValues = new Dictionary<int, string>() { };

        [DataRow("IIV", $"Invalid repetition or subtraction pattern in 'IIV'")]
        [DataRow("XXL", $"Invalid repetition or subtraction pattern in 'XXL'")]
        [TestMethod]
        public void ParseAsIntTest_FormatException(string input, string message)
        {
            var ex = Assert.ThrowsException<FormatException>(() =>
            {
                RomanNumberFactory.ParseAsInt(input);
            });
            Assert.AreEqual(message, ex.Message);
        }
        [TestMethod]
        public void _CheckSymbolsTest()
        {
            string[] testCases = ["IW"];
            CheckPrivateMethod("_CheckSymbols", testCases);

        }
        [TestMethod]
        public void _CheckFormatTest()
        {
            CheckPrivateMethod("_CheckFormat",
                ["IIX"]);

        }
        [TestMethod]
        public void _CheckValidityTest()
        {
            CheckPrivateMethod("_CheckValidity",
                ["IXIX", "IXX", "IVIV", "XCC", "IXIV", "XCXL", "CMCD"]);
        }
        [TestMethod]
        public void _CheckPairsTest()
        {
            CheckPrivateMethod("_CheckPairs",
                ["IM"]);
        }
        private void CheckPrivateMethod(string methodName, string[] testCases)
        {
            Type? rnType = typeof(RomanNumberFactory);
            MethodInfo? m1Info = rnType.GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Static);

            m1Info?.Invoke(null, ["IX"]);

            foreach (var testCase in testCases)
            {

                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [testCase]),
                    $"{methodName} '{testCase}' must throw FormatException"
                );

                Assert.IsInstanceOfType<FormatException>(
                    ex.InnerException,
                    $"{methodName}:FormatException from InnerException");
            }
        }
        [TestMethod]
        public void ParseTest()
        {
            var assertThrowsExceptionMethods =
                typeof(Assert).GetMethods()
                    .Where(x => x.Name == "ThrowsException")
                    .Where(x => x.IsGenericMethod);

            var assertThrowsExceptionFuncStringMethod =
                assertThrowsExceptionMethods
                    .Skip(3)
                    .FirstOrDefault();

            var ex1Template = "Invalid symbol '{0}' in position {1}";
            var exSrcTemplate = "Parse('{0}')";
            var ex2Template = "Invalid order '{0}' before '{1}' in position {2}";
            var formatExceptionType = typeof(FormatException);

            TestCase[] testCases = [
                new() { Source = "N", Value = 0 },
                new() { Source = "I", Value = 1 },
                new() { Source = "II", Value = 2 },
                new() { Source = "III", Value = 3 },
                new() { Source = "IIII", Value = 4 },
                new() { Source = "IV", Value = 4 },
                new() { Source = "VI", Value = 6 },
                new() { Source = "VII", Value = 7 },
                new() { Source = "VIII", Value = 8 },
                new() { Source = "IX", Value = 9 },
                new() { Source = "D", Value = 500 },
                new() { Source = "M", Value = 1000 },
                new() { Source = "CM", Value = 900 },
                new() { Source = "MC", Value = 1100 },
                new() { Source = "MCM", Value = 1900 },
                new() { Source = "MM", Value = 2000 },

                new() { Source = "W", ExceptionMessageParts = [String.Format(exSrcTemplate, "W"), String.Format(ex1Template, 'W', 0),], ExceptionType = formatExceptionType },
                new() { Source = "Q", ExceptionMessageParts = [String.Format(exSrcTemplate, "Q"), String.Format(ex1Template, 'Q', 0),], ExceptionType = formatExceptionType },
                new() { Source = "s", ExceptionMessageParts = [String.Format(exSrcTemplate, "s"), String.Format(ex1Template, 's', 0),], ExceptionType = formatExceptionType },
                new() { Source = "sX", ExceptionMessageParts = [String.Format(exSrcTemplate, "sX"), String.Format(ex1Template, 's', 0),], ExceptionType = formatExceptionType },
                new() { Source = "Xd", ExceptionMessageParts = [String.Format(exSrcTemplate, "Xd"), String.Format(ex1Template, 'd', 1),], ExceptionType = formatExceptionType },

                new() { Source = "IM", ExceptionMessageParts = [String.Format(ex2Template, 'I', 'M', 0)], ExceptionType = formatExceptionType },
                new() { Source = "XIM", ExceptionMessageParts = [String.Format(ex2Template, 'I', 'M', 1)], ExceptionType = formatExceptionType },
                new() { Source = "IMX", ExceptionMessageParts = [String.Format(ex2Template, 'I', 'M', 0)], ExceptionType = formatExceptionType },
                new() { Source = "XMD", ExceptionMessageParts = [String.Format(ex2Template, 'X', 'M', 0)], ExceptionType = formatExceptionType },
                new() { Source = "XID", ExceptionMessageParts = [String.Format(ex2Template, 'I', 'D', 1)], ExceptionType = formatExceptionType },
                new() { Source = "VX", ExceptionMessageParts = [String.Format(ex2Template, 'V', 'X', 0)], ExceptionType = formatExceptionType },
                new() { Source = "VL", ExceptionMessageParts = [String.Format(ex2Template, 'V', 'L', 0)], ExceptionType = formatExceptionType },
                new() { Source = "LC", ExceptionMessageParts = [String.Format(ex2Template, 'L', 'C', 0)], ExceptionType = formatExceptionType },
                new() { Source = "DM", ExceptionMessageParts = [String.Format(ex2Template, 'D', 'M', 0)], ExceptionType = formatExceptionType },

                new() { Source = "IXC", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "VIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "CIIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IIIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "VIIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IVIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IXXC", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IXCM", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "CVIIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "VIXC", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "CIXC", ExceptionMessageParts = [], ExceptionType = formatExceptionType },

                new() { Source = "IXIX", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "IXX", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "IXIV", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCXC", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "CMM", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "CMCD", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCXL", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCC", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCCI", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType }

            ];

            foreach (var testCase in testCases)
                if (testCase.Value is not null)
                {
                    var rn = RomanNumberFactory.Parse(testCase.Source);
                    Assert.IsNotNull(rn);
                    Assert.AreEqual(
                        testCase.Value,
                        rn.Value,
                        $"{testCase.Source} -> {testCase.Value}"
                    );
                }
                else
                {
                    dynamic? ex = assertThrowsExceptionFuncStringMethod?
                        .MakeGenericMethod(testCase.ExceptionType!)
                        .Invoke(null,
                        [
                            () => RomanNumberFactory.Parse(testCase.Source),
                            $"Parse('{testCase.Source}') must throw FormatException"
                        ]);

                    foreach (var exPart in testCase.ExceptionMessageParts ?? [])
                        Assert.IsTrue(
                            ex!.Message.Contains(exPart),
                            $"Parse('{testCase.Source}') FormatException must contain '{exPart}'"
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
    }
    class TestCase
    {
        public String Source { get; set; }
        public int? Value { get; set; }
        public Type? ExceptionType { get; set; }
        public IEnumerable<String?> ExceptionMessageParts { get; set; }
    }
}
