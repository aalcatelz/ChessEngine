using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Chess
    {
        public string Fen { get; private set; }
        private Board Board;
        private Moves Moves;
        private List<FigureMoving> AllMoves;


        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            Fen = fen;
            Board = new Board(fen);
            Moves= new Moves(Board);
            //FindAllMoves();
        }
        private Chess(Board board)
        {
            Board = board;
            Fen = board.Fen;
            Moves = new Moves(Board);
        }

        public Chess Move(string move)
        {
            var fm = new FigureMoving(move);
            if (!Moves.CanMove(fm))
                return this;
            if (Board.IsCheckAfterMove(fm))
                return this;
            var nextBoard = Board.Move(fm);
            var nextChess = new Chess(nextBoard);

            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            var square = new Square(x, y);
            var f = Board.GetFigureAt(square);
            return f == Figure.None ? '.' : (char) f;
        }

        void FindAllMoves()
        {
            var allMoves = new List<FigureMoving>();
            foreach (var fs in Board.YieldFigures())
            {
                foreach (var to in Square.YieldSquares())
                {
                    var fm = new FigureMoving(fs,to);
                    if (Moves.CanMove(fm))
                    {
                        if (!Board.IsCheckAfterMove(fm))
                        allMoves.Add(fm);
                    }
                }
            }

            AllMoves = allMoves;
        }
        public List<string> GetAllMoves()
        {
            FindAllMoves();
            var list = new List<string>();
            foreach (var fm in AllMoves)
            {
                list.Add(fm.ToString());
            }

            return list;
        }

        public bool IsCheck()
        {
            return Board.IsCheck();
        }
    }

    
}
