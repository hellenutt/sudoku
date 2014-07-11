using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller.Util;
using Domain;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class BoardTest
    {
        private const string content = "12345678900000000098765432100000000012345678900000000098765432100000000912345678a";
        private Board board;

        [SetUp]
        public void SetUp()
        {
            board = new Board(content);
        }


        [Test]
        public void Board_can_be_created_with_content()
        {
            Assert.IsNotNull(board);
            Assert.AreEqual(1, board.Squares[0, 0].Number);
            Assert.AreEqual(0, board.Squares[1, 8].Number);
            Assert.AreEqual(9, board.Squares[7, 8].Number);
            Assert.AreEqual(0, board.Squares[8, 8].Number);
        }

        [Test]
        public void Board_has_9_x_9_squares()
        {
            Assert.AreEqual(9, board.Squares.GetLength(0));
            Assert.AreEqual(9, board.Squares.GetLength(1));
        }

        [Test]
        public void It_is_possible_to_get_printable_board()
        {
            Assert.IsNotNullOrEmpty(board.Printable);
        }

        [Test]
        public void Prefilled_squares_are_fixed()
        {
            Assert.IsTrue(board.Squares[0,0].PreFilled);
            Assert.IsFalse(board.Squares[1,0].PreFilled);
        }

        [Test]
        public void Board_should_have_classification()
        {
            Assert.IsTrue(board.Classification, Classification.EASY);
        }
    }
}
