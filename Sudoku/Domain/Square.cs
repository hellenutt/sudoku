using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Square
    {
        private int _number;

        public Square(char character)
        {
            if (char.IsDigit(character) )//&& NumberIsAllowed(character))
            {
                _number = Convert.ToInt32(character.ToString());
            }
            else _number = 0;
        }

        public int Number
        {
            get { return _number; }
        }

        //private bool NumberIsAllowed(int number)
        //{
        //    return number >= min && number <= max;
        //}
    }
}
