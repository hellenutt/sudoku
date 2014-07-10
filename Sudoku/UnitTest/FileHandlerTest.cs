using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller.Util;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class FileHandlerTest
    {
        [Test]
        public void Can_read_fileContent()
        {
            var fileHandler = new FileHandler(@"C:\temp\sudoku\game.txt");
            Assert.IsNotNullOrEmpty(fileHandler.Content);
        }
    }
}
