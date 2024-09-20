﻿using App;
using System.Reflection;

public class RomanNumberFactory
{
    public static RomanNumber Parse(string input)
    {
        return new RomanNumber(ParseAsInt(input));
    }

    public static int ParseAsInt(string input)
    {
        var value = 0;
        var rightDigit = 0;

        _CheckValidity(input);

        foreach (var c in input.Reverse())
        {
            var digit = DigitValue(c.ToString());
            value += digit >= rightDigit ? digit : -digit;
            rightDigit = digit;
        }

        return value;
    }

    private static void _CheckValidity(string input)
    {
        _CheckSymbols(input);
        _CheckPairs(input);
        _CheckFormat(input);
        _CheckSubs(input);
        _CheckSequence(input); //RomanNumberFactoryTest push it row up
    }

    private static void _CheckSubs(string input)
    {
        HashSet<char> subs = new();
        for (var i = 0; i < input.Length - 1; ++i)
        {
            var c = input[i];
            if (DigitValue(c.ToString()) < DigitValue(input[i + 1].ToString()))
            {
                if (subs.Contains(c)) throw new FormatException($"Invalid subtraction repetition: '{c}' in '{input}'");
                subs.Add(c);
            }
        }
    }
    private static void _CheckSequence(string input)
    {
        int digit = 0;
        int rightDigit = 0;
        int previousDigit = 0;

        foreach (char c in input.Reverse())
        {
            try
            {
                digit = DigitValue(c.ToString());
            }
            catch (ArgumentException ex) { }

            if (previousDigit > 0 && digit <= rightDigit && digit < previousDigit)
            {
                throw new FormatException($"Invalid numeral sequence: {c} is smaller than both {rightDigit} and {previousDigit}");
            }

            previousDigit = rightDigit;
            rightDigit = digit;
        }
    }
    private static void _CheckFormat(string input)
    {
        var maxDigit = 0;
        var wasLess = false;
        var wasMax = false;
        foreach (var c in input.Reverse())
        {
            var digit = DigitValue(c.ToString());
            if (digit < maxDigit)
            {
                if (wasLess || wasMax)
                    throw new FormatException($"Invalid repetition or subtraction pattern in '{input}'");
                wasLess = true;
            }
            else if (digit == maxDigit)
            {
                wasMax = true;
                wasLess = false;
            }
            else
            {
                maxDigit = digit;
                wasLess = false;
                wasMax = false;
            }
        }
    }

    private static bool IsValidPair(char left, char right)
    {
        return (left == 'I' && (right == 'V' || right == 'X')) ||
               (left == 'X' && (right == 'L' || right == 'C')) ||
               (left == 'C' && (right == 'D' || right == 'M'));
    }
    private static void _CheckPairs(string input)
    {
        for (var i = 0; i < input.Length - 1; ++i)
        {
            var rightDigit = DigitValue(input[i + 1].ToString());
            var leftDigit = DigitValue(input[i].ToString());
            if (leftDigit != 0 &&
                leftDigit < rightDigit &&
                (rightDigit / leftDigit > 10 || // IC IM
                 leftDigit == 5 || leftDigit == 50 || leftDigit == 500 // VX
                ))
                throw new FormatException(
                    $"Invalid order '{input[i]}' before '{input[i + 1]}' in position {i}");
        }
    }

    private static void _CheckSymbols(string input)
    {
        var pos = 0;
        foreach (var c in input)
        {
            try
            {
                DigitValue(c.ToString());
            }
            catch
            {
                throw new FormatException(
                    $"Parse('{input}') error: Invalid symbol '{c}' in position {pos}");
            }

            pos += 1;
        }
    }
    
    public static int DigitValue(string digit)
    {
        return digit switch
        {
            "N" => 0,
            "I" => 1,
            "V" => 5,
            "X" => 10,
            "L" => 50,
            "C" => 100,
            "D" => 500,
            "M" => 1000,
            _ => throw new ArgumentException(
                $"{nameof(RomanNumberFactory)}::{nameof(DigitValue)}: 'digit' has invalid value '{digit}'")
        };
    }
    
}