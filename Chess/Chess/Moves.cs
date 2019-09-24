using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Moves
    {
        FigureMoving fm;
        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            this.fm = fm;

            return
                CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();
        }

        bool CanFigureMove()
        {
            switch (fm.Figure)
            {
                case Figure.WhiteKing:
                case Figure.BlackKing:
                    return CanKingMove();
                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    return false;
                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return false;
                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return false;
                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();
                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return false;
                default:
                    return false;
            }
        }

        private bool CanKingMove()
        {
            if (fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1)
                return true;
            return false;

        }

        private bool CanKnightMove()
        {
            if (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2) return true;
            if (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1) return true;
            return false;
        }

        bool CanMoveTo()
        {
            return fm.To.OnBoard() &&
                   fm.From != fm.To &&
                   board.GetFigureAt(fm.To).GetColor() != board.MoveColor;
        }

        bool CanMoveFrom()
        {
            return fm.From.OnBoard() &&
                   fm.Figure.GetColor() == board.MoveColor;
        }
    }
}
