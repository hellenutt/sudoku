using Domain;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class BoardTest
    {
        private const string Content = "12345678900000000098765432100000000012345678900000000098765432100000000912345678a";
        private const string EasyContent = "510000083800416005000000000098504610000901000064203570000000000600157004780000096";
        private const string MediumContent = "700090003200468001008000600040020090000304000080010030009000700500142006800050002";
        private Board _board;

        [SetUp]
        public void SetUp()
        {
            _board = new Board(Content);
        }


        [Test]
        public void Board_can_be_created_with_content()
        {
            Assert.IsNotNull(_board);
            Assert.AreEqual(1, _board.Squares[0, 0].Number);
            Assert.AreEqual(0, _board.Squares[1, 8].Number);
            Assert.AreEqual(9, _board.Squares[7, 8].Number);
            Assert.AreEqual(0, _board.Squares[8, 8].Number);
        }

        [Test]
        public void Board_has_9_x_9_squares()
        {
            Assert.AreEqual(9, _board.Squares.GetLength(0));
            Assert.AreEqual(9, _board.Squares.GetLength(1));
        }

        [Test]
        public void It_is_possible_to_get_printable_board()
        {
            Assert.IsNotNullOrEmpty(_board.Printable);
        }

        [Test]
        public void Prefilled_squares_are_fixed()
        {
            foreach (var square in _board.Squares)
            {
                if (square.Number == 0)
                    Assert.IsFalse(square.ReadOnly);
                else
                {
                    Assert.IsTrue(square.ReadOnly);
                }
            }
            Assert.IsTrue(_board.Squares[0,0].ReadOnly);
            Assert.IsFalse(_board.Squares[1,0].ReadOnly);
        }

        [Test]
        public void Board_should_have_easy_classification()
        {
            var easyBoard = new Board(EasyContent);
            Assert.AreEqual(Classification.Easy, easyBoard.Classification);
        }

        [Test]
        public void Board_should_have_medium_classification()
        {
            var mediumBoard = new Board(MediumContent);
            Assert.AreEqual(Classification.Medium, mediumBoard.Classification);
        }
    }
}
