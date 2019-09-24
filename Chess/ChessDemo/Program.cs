using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;

namespace ChessDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var chess = new Chess.Chess();

            while (true)
            {
                Console.WriteLine(chess.Fen);
                Print(ChessToAscii(chess));
                var move = Console.ReadLine();
                if (move == "") break;
                chess = chess.Move(move);
            }
        }

        static string ChessToAscii(Chess.Chess chess)
        {
            string text = "  +----------------+\n";
            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++)
                {
                    text += chess.GetFigureAt(x, y) + " ";
                }

                text += "|\n";
            }
            text += "  +----------------+\n";
            text += "    a b c d e f g h\n";

            return text;
        }
        static void Print(string text)
        {
            var oldForeColor = Console.ForegroundColor;

            foreach (var x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }

            Console.ForegroundColor = oldForeColor;
        }
    }
}
