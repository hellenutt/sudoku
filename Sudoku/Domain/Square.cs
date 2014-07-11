using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class Square
    {
        private int _number;
        private bool _preFilled;

        public Square(char character)
        {
            if (char.IsDigit(character) && character != '0')
            {
                _number = Convert.ToInt32(character.ToString());
                _preFilled = true;
            }
            else _number = 0;
        }

        public void SetNumber(int number)
        {
            _number = number;
        }

        public int Number
        {
            get { return _number; }
        }

        public bool PreFilled
        {
            get { return _preFilled; }
        }
    }
}
