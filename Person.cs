using System.Collections.Generic;

namespace Blackjack_v3
{
    class Person
    {
        #region Fields

        private string _gameRole;

        #endregion

        #region Constructors
        public Person(string gameRole)
        {
            _gameRole = gameRole;
        }

        #endregion

        #region Properties
        public List<Card> CardsOnHand { get; } = new();

        public string GameRole
        {
            get { return _gameRole; }
            set { GameRole = value; }
        }

        #endregion

        #region Methods
        public void AddCardToHand(Card c)
        {
            CardsOnHand.Add(c);
        }

        #endregion
    }
}