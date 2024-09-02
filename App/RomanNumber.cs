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
            for (int i = 0; i < 10; i++)
            {
                if(input == RomanNaturalNumbers[i])
                {
                    return new RomanNumber(Convert.ToInt32(i+1));
                }
            }
            throw new Exception("That is not a valid Roman numeral within the natural numbers range.");
        }
    }

    






}
