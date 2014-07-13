using System;
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
                Console.WriteLine("Please specify file path as the first argument.");
            }
            else
            {
                var fileHandler = new FileHandler(args[0]);
                var board = new Board(fileHandler.Content);
                var gameEngine = new GameEngine();
                Console.Write(board.Printable);
                Console.WriteLine();
                var solved = gameEngine.Solve(board);
                Console.WriteLine();
                Console.WriteLine("Solved: " + solved);
                Console.WriteLine(board.Printable);
            }
        }
    }
}
