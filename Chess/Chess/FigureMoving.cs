using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class FigureMoving
    {
        public Figure Figure { get; private set; }
        public Square From { get; private set; }
        public Square To { get; private set; }
        public Figure Promotion { get; private set; }

        public FigureMoving(FigureOnSquare fs, Square to, Figure promotion = Figure.None)
        {
            Figure = fs.Figure;
            From = fs.Square;
            To = to;
            Promotion = promotion;
        }

        public FigureMoving(string move) //Pe2e4   Pe7e8Q
        {
            Figure = (Figure) move[0];
            From = new Square(move.Substring(1, 2));
            To = new Square(move.Substring(3, 2));
            Promotion = (move.Length == 6) ? (Figure) move[5] : Figure.None;
        }

        public int DeltaX => To.x - From.x;
        public int DeltaY => To.y - From.y;

        public int AbsDeltaX => Math.Abs(DeltaX);
        public int AbsDeltaY => Math.Abs(DeltaY);

        public int SignX => Math.Sign(DeltaX);
        public int SignY => Math.Sign(DeltaY);

        public override string ToString()
        {
            var text = (char) Figure + From.Name + To.Name;
            if (Promotion != Figure.None)
            {
                text += (char) Promotion;
            }

            return text;
        }
    }
}
