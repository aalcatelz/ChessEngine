using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    enum Figure
    {
        None,

        WhiteKing = 'K',
        WhiteQueen = 'Q',
        WhiteRook = 'R',
        WhiteBishop  = 'B',
        WhiteKnight = 'N',
        WhitePawn = 'P',

        BlackKing = 'k',
        BlackQueen = 'q',
        BlackRook = 'r',
        BlackBishop = 'b',
        BlackKnight = 'n',
        BlackPawn = 'p'
    }

    static class FigureMethods
    {
        public static Color GetColor(this Figure figure)
        {
            switch (figure)
            {
                case Figure.None:
                    return Color.None;
                case Figure.WhiteKing:
                case Figure.WhiteQueen:
                case Figure.WhiteRook:
                case Figure.WhiteBishop:
                case Figure.WhiteKnight:
                case Figure.WhitePawn:
                    return Color.White;
                default:
                    return Color.Black;
            }
            
        }
    }
}
