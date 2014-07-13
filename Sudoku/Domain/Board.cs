using System.Text;

namespace Domain
{
    public class Board
    {
        public Square[,] Squares { get; set; }
        public Classification Classification {
            get
            {
                var numberOfPreFilled = 0;
                foreach (var square in Squares)
                {
                    if (square.ReadOnly) numberOfPreFilled++;
                }
                if (31 <= numberOfPreFilled && numberOfPreFilled <= 35)
                    return Classification.Easy;
                if (25 <= numberOfPreFilled && numberOfPreFilled <= 30)
                    return Classification.Medium;
                return Classification.Unknown;
            }
        }

        public int Size
        {
            get { return 9; }
        }

        public Board(string content)
        {
            Squares = new Square[Size,Size];
            ;
            var i = 0;
            var j = 0;

            foreach (var character in content)
            {
                Squares[i, j] = new Square(character);
                j++;
                if (j >= Size)
                {
                    j = 0;
                    i ++;
                    if (i >= Size)
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
                    var print = square.Number.ToString();
                    if (print == "0")
                        print = ".";
                    printable.Append(print);
                    i++;
                }
                return printable.ToString();
            }
        }
    }

    public enum Classification
    {
        Easy,
        Medium,
        Unknown
    }
}
