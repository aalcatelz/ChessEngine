using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board
    {
        public string Fen { get; private set; }
        Figure[,] figures;
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public Board(string fen)
        {
            Fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        void Init()
        {
            // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            // 0                                           1 2    3 4 5 
            //2 3 4 will not implemented

            var parts = Fen.Split();
            if (parts.Length!=6) return;

            InitFigures(parts[0]);
            MoveColor = parts[1] == "b" ? Color.Black : Color.White;
            MoveNumber = int.Parse(parts[5]);
        }

        void InitFigures(string data)
        {
            for (int j = 8; j>=2; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }

            data = data.Replace("1", ".");
            var lines = data.Split('/');

            for (int y = 7; y >=0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    figures[x, y] = lines[7 - y][x] == '.' ? Figure.None : (Figure) lines[7 - y][x];
                }
            }
        }

        void GenerateFen()
        {
            Fen = FenFigures() + " " +
                  (MoveColor == Color.White ? "w" : "b") +
                  " - - 0 " + MoveNumber;
        }

        public Figure GetFigureAt(Square square)
        {
            return square.OnBoard() ? figures[square.x, square.y] : Figure.None;
        }

        string FenFigures()
        {
            var sb = new StringBuilder();

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(figures[x, y] == Figure.None ? '1' : (char) figures[x, y]);
                }
                if (y > 0)
                    sb.Append('/');
            }

            var eight = "11111111";
            for (int j = 8; j >=2; j--)
            {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }

            return sb.ToString();
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                figures[square.x, square.y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            var next = new Board(Fen);
            next.SetFigureAt(fm.From, Figure.None);
            next.SetFigureAt(fm.To, fm.Promotion == Figure.None ? fm.Figure : fm.Promotion);
            if (MoveColor == Color.Black)
            {
                next.MoveNumber++;
            }

            next.MoveColor = MoveColor.FlipColor();
            next.GenerateFen();

            return next;
        }

        public bool IsCheck()
        {
            var after = new Board(Fen);
            after.MoveColor = MoveColor.FlipColor();
            return after.CanEatKing();
        }

        private bool CanEatKing()
        {
            var badKing = FindBadKing();
            var moves = new Moves(this);
            foreach (var fs in YieldFigures())
            {
                var fm = new FigureMoving(fs, badKing);
                if (moves.CanMove(fm))
                    return true;
            }

            return false;
        }

        private Square FindBadKing()
        {
            var badKing = MoveColor == Color.Black ? Figure.WhiteKing : Figure.BlackKing;
            foreach (var square in Square.YieldSquares())
            {
                if (GetFigureAt(square) == badKing)
                    return square;
            }
            return Square.None;
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (var square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor()==MoveColor)
                    yield return new FigureOnSquare(GetFigureAt(square),square);
            }
        }

        public bool IsCheckAfterMove(FigureMoving fm)
        {
            var after = Move(fm);
            return after.CanEatKing();
        }
    }
}
