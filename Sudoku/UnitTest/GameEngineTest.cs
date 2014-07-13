using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Domain;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class GameEngineTest
    {
        private GameEngine _gameEngine;
        private Board _board;
        
        [SetUp]
        public void SetUp()
        {
            var content = "510000083800416005000000000098504610000901000064203570000000000600157004780000096";
            _board = new Board(content);
            _gameEngine = new GameEngine();
        }

        [Test]
        public void It_should_be_possible_to_make_a_valid_move()
        {
            Assert.IsTrue(_gameEngine.Move(_board, 0, 3, 7));
            Assert.AreEqual(7, _board.Squares[0,3].Number);
        }

        [Test]
        public void Move_outside_board_is_not_ok()
        {
            Assert.IsFalse(_gameEngine.Move(_board, -2, 9, 3));
            Assert.IsFalse(_gameEngine.Move(_board, 0, 10, 3));
            Assert.IsFalse(_gameEngine.Move(_board, 9, 1, 3));
        }

        [Test]
        public void Move_on_prefilled_square_is_not_ok()
        {
            Assert.IsFalse(_gameEngine.Move(_board, 8, 8, 4));
        }

        [Test]
        public void Move_with_number_already_in_row_is_not_ok()
        {
            Assert.IsFalse(_gameEngine.Move(_board, 0, 2, 3));
        }

        [Test]
        public void Move_with_number_already_in_col_is_not_ok()
        {
            Assert.IsFalse(_gameEngine.Move(_board, 2, 3, 4));
        }

        [Test]
        public void Move_with_number_already_in_group_is_not_ok()
        {
            Assert.IsFalse(_gameEngine.Move(_board, 1, 6, 3));
        }

        [Test]
        public void Move_with_unvalid_number_is_not_ok()
        {
            Assert.IsFalse(_gameEngine.Move(_board, 0, 0, 10));
        }

        [Test]
        public void It_should_be_possible_to_solve_the_sudoku()
        {
            var solved = _gameEngine.Solve(_board);
            Console.Out.Write(_board.Printable);
            Assert.IsTrue(solved, "Sudoku was not solvable");
        }

        [Test]
        public void Easy_sudoku_should_be_possible_to_generate()
        {
            var board = _gameEngine.Generate(Classification.Easy);
            Assert.IsNotNull(board);
            Assert.AreEqual(Classification.Easy, board.Classification, "Classification of generated sudoku was incorrect");
        }

        [Test]
        public void Medium_sudoku_should_be_possible_to_generate()
        {
            var board = _gameEngine.Generate(Classification.Medium);
            Assert.IsNotNull(board);
            Assert.AreEqual(Classification.Medium, board.Classification, "Classification of generated sudoku was incorrect");
        }

        [Test]
        public void Generated_sudoku_should_be_possible_to_solve()
        {
            var board = _gameEngine.Generate(Classification.Easy);
            var solved = _gameEngine.Solve(board);
            Assert.IsTrue(solved, "The generated sudoku could not be solved.");
        }
    }
}
