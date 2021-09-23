using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_v3
{
    class Card
    {
        #region Fields

        private string _type;
        private int _value;
        private bool _isSoftAce;

        #endregion

        #region Constructors

        public Card(string type, int value)
        {
            _type = type;
            _value = value;
        }

        public Card(string type, int value, bool isSoftAce)
        {
            _type = type;
            _value = value;
            _isSoftAce = isSoftAce;
        }

        #endregion

        #region Properties
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool IsSoftAce
        {
            get { return _isSoftAce; }
            set { _isSoftAce = value; }
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{_type}, {_value}";
        }

        #endregion
    }
}