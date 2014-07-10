using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Board
    {
        public Square[,] Squares { get; set; }
        private int rows = 9;
        private int cols = 9;

        public Board(string content)
        {
            Squares = new Square[rows,cols];
            ;
            var i = 0;
            var j = 0;

            foreach (var character in content)
            {
                    Squares[i,j] = new Square(character);
                j++;
                if (j >= cols)
                {
                    j = 0;
                    i ++;
                    if (i >= rows)
                        break;
                }
            }
            
        }

        public string Printable
        {
            get
            {
                var printable = new StringBuilder();
                var i = 0;
                foreach (var square in Squares)
                {
                    if (i%3 == 0)
                    {
                        if (i%27 == 0)
                        {
                            printable.Append("\n\n");
                        }
                        else if (i%9 == 0)
                        {
                            printable.Append("\n");
                        }
                        else
                        {
                            printable.Append(" ");
                        }
                    }
                    printable.Append(square.Number);
                    i++;
                }
                return printable.ToString();
            }
        }
    }
}
