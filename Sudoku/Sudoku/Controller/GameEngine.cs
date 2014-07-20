using System;
using System.Collections.Generic;
using Domain;

namespace Controller
{
    public class GameEngine
    {
        /// <summary>
        /// Makes a move on given board.
        /// </summary>
        /// <returns>Returns true if the move was made. Returns false if the move
        /// was not made (i.e. invalid move).</returns>
        public bool Move(Board board, int row, int col, int number)
        {
            // Check if index is outside board
            if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
            {
                return false;
            }
            // Check if valid move
            if (!IsAValidMove(board, row, col, number))
                return false;

            board.Squares[row, col].SetNumber(number);
            return true;
        }

        /// <summary>
        /// Solves the sudoku.
        /// </summary>
        /// <returns>Returns true if the game is solvable.</returns>
        public bool Solve(Board board)
        {
            int row;
            int col;
            FindEmptySquare(board, out row, out col);
            if (row < 0 && col < 0)
                return true;

            for (int i = 1; i < 10; i++)
            {
                if (Move(board, row, col, i))
                {
                    if (Solve(board))
                        return true;

                    // Can't find a valid number, reset number to zero and continue.
                    board.Squares[row, col].SetNumber(0);
                }
            }

            return false;
        }

        /// <summary>
        /// Generates a new sudoku board with given classification.
        /// </summary>
        /// <param name="classification">Classification. Support only for easy and medium.</param>
        /// <returns></returns>
        public Board Generate(Classification classification)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            //1. Pick a solution and transform it randomly.
            var solutionIndex = random.Next(0, 2);

            //Initial transformation:
            var transformations = Enum.GetValues(typeof(Transformations));
            var randomTransformation = (Transformations)transformations.GetValue(random.Next(transformations.Length));
            var transformedSolution = Transform(_boardSolutions[solutionIndex], randomTransformation);
            // 10 additional transformations:
            for (var i = 0; i < 10; i++)
            {
                randomTransformation = (Transformations)transformations.GetValue(random.Next(transformations.Length));
                transformedSolution = Transform(transformedSolution, randomTransformation);
            }
           
            //2. Empty some squares
            var nbrToRemove = 0;
            switch (classification)
            {
                case Classification.Easy:
                    nbrToRemove = 50;
                    break;
                case Classification.Medium:
                    nbrToRemove = 55;
                    break;
            }

            var usedIndex = new List<int>();
            for (var i = 0; i < nbrToRemove; i++)
            {
                var indexToRemove = random.Next(0, 81);
                while (usedIndex.Contains(indexToRemove))
                    indexToRemove = random.Next(0, 81);
                usedIndex.Add(indexToRemove);
                transformedSolution[indexToRemove] = 0;
            }

            //3. Create a Board object from the transformed int array
            return new Board(string.Join(string.Empty, transformedSolution));
        }

        /// <summary>
        /// Checks if the move is valid. A move is NOT valid if:
        /// * the square is read only
        /// * the row already contains the number
        /// * the column already contains the number
        /// * the 3x3 group already contains the number
        /// </summary>
        /// <returns>Returns true if it is a valid move.</returns>
        private bool IsAValidMove(Board board, int row, int col, int number)
        {
            //Not valid move if readonly
            if (board.Squares[row, col].ReadOnly)
                return false;
            //Not valid move if row contains number
            for (var i = 0; i < board.Size; i++)
            {
                if (i != col && board.Squares[row, i].Number == number)
                {
                    return false;
                }
            }
            //Not valid move if col contains number
            for (var i = 0; i < board.Size; i++)
            {
                if (i != row && board.Squares[i, col].Number == number)
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
                    if (board.Squares[i, j].Number == number)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the current 3x3 group.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>Returns the number of the group. From top left group is group 0 and bottom right is group 8.</returns>
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

        /// <summary>
        /// Finds an emtpy square of the board, of any.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <param name="row">Output parameter row of the empty square. -1 if no empty square left.</param>
        /// <param name="col">Output parameter col of the empty square. -1 if no empty square left.</param>
        private void FindEmptySquare(Board board, out int row, out int col)
        {
            row = -1;
            col = -1;
            for (int i = 0; i < board.Size; i++)
            {
                if (row > -1)
                    break;
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.Squares[i, j].Number == 0)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Perform a given transformation of the array.
        /// </summary>
        /// <returns>Returns the transformed array.</returns>
        private int[] Transform(int[] solution, Transformations transformation)
        {
            int tmp;
            switch (transformation)
            {
                case Transformations.Vertical:
                    for (var i = 0; i < 81; i++)
                    {
                        // If column (i % 9) < 4 swap it:
                        if (i%9 < 4)
                        {
                            tmp = solution[i];
                            var div9 = Math.Floor((decimal)i/9);
                            var tmpIx = (int) ((9*div9 + 8) - (i - (9*div9)));
                            solution[i] = solution[tmpIx];
                            solution[tmpIx] = tmp;
                        }
                    }
                    break;
                case Transformations.MainDiagonal:
                    for (var i = 0; i < 81; i++)
                    {
                        // Upper Main diagonal: row + column < 8
                        if ((Math.Floor((decimal)i/9) + i%9) < 8)
                        {
                            var mod9 = Math.Floor((decimal)i%9);
                            var div9 = Math.Floor((decimal)i/9);
                            tmp = solution[i];
                            var tmpIx = (int)((8 - mod9)*9 + 8 - div9);
                            solution[i] = solution[tmpIx];
                            solution[tmpIx] = tmp;
                        }
                    }
                    break;
                case Transformations.MinorDiagonal:

                    for (var i = 0; i < 81; i++)
                    {
                        // Upper Minor diagonal: row > column
                        if (Math.Floor((decimal)i/9) > i%9)
                        {
                            var mod9 = Math.Floor((decimal)i%9);
                            var div9 = Math.Floor((decimal)i/9);
                            tmp = solution[i];
                            var tmpIx = (int)(div9 + mod9*9);
                            solution[i] = solution[tmpIx];
                            solution[tmpIx] = tmp;
                        }
                    }
                    break;
                case Transformations.Horizontal:
                default:
                    for (var i = 0; i < 81; i++)
                    {
                        // Row < 4
                        if (Math.Floor((decimal)i/9) < 4)
                        {
                            var mod9 = Math.Floor((decimal)i % 9);
                            var div9 = Math.Floor((decimal)i / 9);
                            tmp = solution[i];
                            var tmpIx = (int)(mod9 + (8 - div9)*9);
                            solution[i] = solution[tmpIx];
                            solution[tmpIx] = tmp;
                        }
                    }
                    break;
            }
            return solution;
        }

        private readonly int[][] _boardSolutions =
        {
            new[]
            {
                5, 1, 6, 7, 2, 9, 4, 8, 3, 8, 7, 3, 4, 1, 6, 9, 2, 5, 9, 4, 2, 8, 3, 5, 7, 6, 1, 3, 9, 8, 5, 7, 4, 6, 1,
                2, 2, 5, 7, 9, 6, 1, 3, 4, 8, 1, 6, 4, 2, 8, 3, 5, 7, 9, 4, 3, 1, 6, 9, 8, 2, 5, 7, 6, 2, 9, 1, 5, 7, 8,
                3,
                4, 7, 8, 5, 3, 4, 2, 1, 9, 6
            },
            new[]
            {
                7, 5, 6, 2, 9, 1, 8, 4, 3, 2, 9, 3, 4, 6, 8, 5, 7, 1, 4, 1, 8, 5, 7, 3, 6, 2, 9, 3, 4, 5, 6, 2, 7, 1, 9,
                8,
                9, 7, 1, 3, 8, 4, 2, 6, 5, 6, 8, 2, 9, 1, 5, 4, 3, 7, 1, 2, 9, 8, 3, 6, 7, 5, 4, 5, 3, 7, 1, 4, 2, 9, 8,
                6,
                8, 6, 4, 7, 5, 9, 3, 1, 2
            }
        };
    }

    public enum Transformations
    {
        Vertical,
        Horizontal,
        MainDiagonal,
        MinorDiagonal
    };
}
