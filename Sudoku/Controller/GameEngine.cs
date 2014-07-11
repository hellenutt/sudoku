using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Controller
{
    public class GameEngine
    {
        private Board _board;

        public GameEngine(Board board)
        {
            _board = board;
        }

        public bool Move(int row, int col, int number)
        {
            // Check if index is outside board
            if (row < 0 || row >= _board.Size || col < 0 || col >= _board.Size)
            {
                return false;
            }
            // Check if valid move
            if (!IsAValidMove(row, col, number))
                return false;

            _board.Squares[row, col].SetNumber(number);
            return true;
        }

        private bool IsAValidMove(int row, int col, int number)
        {
            //Not valid move if prefilled
            if (_board.Squares[row, col].PreFilled)
                return false;
            //Not valid move if row contains number
            for (var i = 0; i < _board.Size; i++)
            {
                if (i != col && _board.Squares[row, i].Number == number)
                {
                    return false;
                }
            }
            //Not valid move if col contains number
            for (var i = 0; i < _board.Size; i++)
            {
                if (i != row && _board.Squares[i, col].Number == number)
                {
                    return false;
                }
            }
            //Not valid move if group contains number
            var box = GetBox(row, col);
            var rows = 0;
            var cols = 0;
            if (box >= 3 && box < 6)
                rows = 3;
            else if (box >= 6 && box < 9)
                rows = 6;
            if (box == 1 || box == 4 || box == 7)
                cols = 3;
            else if (box == 2 || box == 5 || box == 8)
                cols = 6;

            for (var i = rows; i < rows + 3; i++)
            {
                for (var j = cols; j < cols + 3; j++)
                {
                    if (_board.Squares[i, j].Number == number)
                        return false;
                }
            }

            return true;
        }

        private int GetBox(int row, int col)
        {
            if (row < 3)
            {
                if (col < 3)
                    return 0;
                if (col < 6)
                    return 1;
                if (col < 9)
                    return 2;
            }
            else if (row < 6)
            {
                if (col < 3)
                    return 3;
                if (col < 6)
                    return 4;
                if (col < 9)
                    return 5;
            }
            else if (row < 9)
            {
                if (col < 3)
                    return 6;
                if (col < 6)
                    return 7;
                if (col < 9)
                    return 8;
            }
            return -1;
        }

        public bool Solve()
        {
            int row;
            int col;
            FindEmptySquare(out row, out col);
            if (row < 0 && col < 0)
                return true;

            for (int i = 1; i < 10; i++)
            {
                if (Move(row, col, i))
                {
                    if (Solve())
                        return true;

                    // Can't find a valid number, reset number to zero and continue.
                    _board.Squares[row,col].SetNumber(0);
                }
            }

            return false;
        }

        private void FindEmptySquare(out int row, out int col)
        {
            row = -1;
            col = -1;
            for (int i = 0; i < _board.Size; i++)
            {
                if (row > -1)
                    break;
                for (int j = 0; j < _board.Size; j++)
                {
                    if (_board.Squares[i, j].Number == 0)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }
        }
    }
}
