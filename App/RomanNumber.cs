using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int value)
    {

        private readonly int _value = value;
        public int Value => _value;
        public static RomanNumber Parse(String input)
        {
            if(string.IsNullOrEmpty(input)) 
                throw new ArgumentException("Input cannot be null or empty", nameof(input));

            int value = 0;
            int prevDigit = 0;
            int pos = input.Length;
            List<string> errors = new();
            foreach (char c in input.Reverse())
            {
                pos -=1;
                int digit;
                try
                {
                    digit = DigitValue(c.ToString());

                }
                catch
                {
                    errors.Add($"Invalid symbol '{c}' in position {pos}");
                    continue;
                }
                if (digit != 0 && prevDigit/digit>10)
                {
                    errors.Add($"Invalid order '{c}' before '{input[pos + 1]}' in position {pos}");
                }

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
            if (errors.Any())
            {
                throw new FormatException(string.Join("; ", errors));
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
             _ => throw new ArgumentException($"{nameof(RomanNumber)}::{nameof(DigitValue)}:'digit' has invalid value '{digit}'")
        };
        public override string ToString()
        {
            if (_value == 0) return "N";
            Dictionary<int, string> parts = new Dictionary<int, string>()
            {
                {1000, "M" },
                {900, "CM" },
                {500, "D" },
                {400, "CD" },
                {100, "C" },
                {90, "XC" },
                {50, "L"},
                {40, "XL"},
                {10, "X"},
                {9, "IX"},
                {5, "V"},
                {4, "IV"},
                {1, "I"},
            };

            int v = _value;
            StringBuilder sb = new StringBuilder();
            foreach(var part in parts)
            {
                while (v >= part.Key)
                {
                    v-= part.Key;
                    sb.Append(part.Value);
                }
            }
            return sb.ToString();
        }
    }

}
