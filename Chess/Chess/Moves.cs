﻿using System;
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
                    return CanStraightMove();
                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return (fm.SignX == 0 || fm.SignY == 0) && CanStraightMove();
                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0) && CanStraightMove();
                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();
                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return CanPawnMove();
                default:
                    return false;
            }
        }

        private bool CanPawnMove()
        {
            if (fm.From.y < 1 || fm.From.y > 6)
            {
                return false;
            }

            var stepY = fm.Figure.GetColor() == Color.White ? 1 : -1;

            return
                CanPawnGo(stepY) ||
                CanPawnJump(stepY) ||
                CanPawnEat(stepY);

        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(fm.To) != Figure.None)
                if (fm.AbsDeltaX == 1)
                    if (fm.DeltaY == stepY)
                        return true;
            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(fm.To) == Figure.None)
                if (fm.DeltaX == 0)
                    if (fm.DeltaY == stepY * 2)
                        if (fm.From.y == 1 || fm.From.y == 6)
                            if (board.GetFigureAt(new Square(fm.From.x, fm.From.y + stepY)) == Figure.None)
                                return true;
            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(fm.To) == Figure.None)
                if (fm.DeltaX == 0)
                    if (fm.DeltaY == stepY)
                        return true;
            return false;
        }

        private bool CanStraightMove()
        {
            var at = fm.From;
            do
            {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);
                if (at == fm.To)
                {
                    return true;
                }
            } while (at.OnBoard() &&
                     board.GetFigureAt(at) == Figure.None);

            return false;
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
