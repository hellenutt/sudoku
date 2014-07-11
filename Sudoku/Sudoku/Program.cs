using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Controller.Util;
using Domain;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify file path");
            }
            else
            {
                var fileHandler = new FileHandler(args[0]);
                var board = new Board(fileHandler.Content);
                var gameEngine = new GameEngine(board);
                Console.Write(board.Printable);
                Console.WriteLine();
                var solved = gameEngine.Solve();
                Console.WriteLine("Solved? " + solved);
                Console.WriteLine(board.Printable);

            }
        }
    }
}
