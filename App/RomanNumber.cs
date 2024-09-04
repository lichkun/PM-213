using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int value)
    {
        private static string[] RomanNaturalNumbers = 
        {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };


        private readonly int _value = value;
        public int Value => _value;
        public static RomanNumber Parse(String input)
        {
            if(string.IsNullOrEmpty(input)) 
                throw new ArgumentException("Input cannot be null or empty", nameof(input));

            int value = 0;
            int prevDigit = 0;

            foreach (char c in input.Reverse())
            {
                int digit = DigitValue(c.ToString());
                if (digit >= prevDigit)
                {
                    value += digit;
                }
                else
                {
                    value -= digit;
                }
                prevDigit = digit;
            }
            return new RomanNumber(value);
        }
        public static int DigitValue(String digit) => digit switch
        {
            "N" => 0,
            "I" => 1,
            "V" => 5,
            "X" => 10,
            "L" => 50,
            "C" => 100,
            "D" => 500,
            "M" => 1000,
             _ => throw new ArgumentException("Invalid Roman digit.")
        };
    }

}
