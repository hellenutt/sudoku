using System;

namespace Domain
{
    public class Square
    {
        private int _number;
        private readonly bool _readOnly;

        public Square(char character)
        {
            if (char.IsDigit(character) && character != '0')
            {
                _number = Convert.ToInt32(character.ToString());
                _readOnly = true;
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

        public bool ReadOnly
        {
            get { return _readOnly; }
        }
    }
}
