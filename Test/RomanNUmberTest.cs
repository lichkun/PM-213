using App;

namespace Test
{
    [TestClass]
    public class RomanNUmberTest
    {
        [TestMethod]
        public void TestParse()
        {
            var cases = new (string input, int expected)[]
            {
                ("I", 1),
                ("IV", 4),
                ("IX", 9),
                ("X", 10),
            };

            foreach (var (input, expected) in cases)
            {
                RomanNumber result = RomanNumber.Parse(input);
                Assert.AreEqual(expected, result.Value, $"Failed for input: {input}");
            }
        }
        

    }
}