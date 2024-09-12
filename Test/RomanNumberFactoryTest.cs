using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class RomanNumberFactoryTest
    {
        [DataRow("IIV", "Invalid numeral sequence: I is smaller than both 1 and 5")]
        [DataRow("XXL", "Invalid numeral sequence: X is smaller than both 10 and 50")]
        [TestMethod]
        public void ParseAsIntTest_FormatException(string input, string message)
        {
            var ex = Assert.ThrowsException<FormatException>(() =>
            {
                RomanNumberFactory.ParseAsInt(input);
            });
            Assert.AreEqual(message, ex.Message);
        }
    }
}
