using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static class RomanNumberExtension 
    {
        public static RomanNumber Plus(this RomanNumber rn, params RomanNumber[] other)
        {
            return RomanNumberMath.Plus([rn, ..other]);
        }
    }
}
