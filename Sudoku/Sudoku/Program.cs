using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Console.Write(board.Printable);
            }
        }
    }
}
