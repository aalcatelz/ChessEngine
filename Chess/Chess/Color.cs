using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    enum Color
    {
        None,
        White,
        Black
    }

    static class ColorMethods
    {
        public static Color FlipColor(this Color color)
        {
            switch (color)
            {
                case Color.Black:
                    return Color.White;
                case Color.White:
                    return Color.Black;
                default:
                    return Color.None;
            }
        }
    }
}
