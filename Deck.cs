using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_v3
{
    class Deck
    {
        #region Fields

        private readonly List<Card> _deck = new()
        {
            new Card("Two of Hearts", 2),
            new Card("Three of Hearts", 3),
            new Card("Four of Hearts", 4),
            new Card("Five of Hearts", 5),
            new Card("Six of Hearts", 6),
            new Card("Seven of Hearts", 7),
            new Card("Eight of Hearts", 8),
            new Card("Nine of Hearts", 9),
            new Card("Ten of Hearts", 10),
            new Card("Jack of Hearts", 10),
            new Card("Queen of Hearts", 10),
            new Card("King of Hearts", 10),
            new Card("Ace of Hearts", 11, false),

            new Card("Two of Spades", 2),
            new Card("Three of Spades", 3),
            new Card("Four of Spades", 4),
            new Card("Five of Spades", 5),
            new Card("Six of Spades", 6),
            new Card("Seven of Spades", 7),
            new Card("Eight of Spades", 8),
            new Card("Nine of Spades", 9),
            new Card("Ten of Spades", 10),
            new Card("Jack of Spades", 10),
            new Card("Queen of Spades", 10),
            new Card("King of Spades", 10),
            new Card("Ace of Spades", 11, false),

            new Card("Two of Clubs", 2),
            new Card("Three of Clubs", 3),
            new Card("Four of Clubs", 4),
            new Card("Five of Clubs", 5),
            new Card("Six of Clubs", 6),
            new Card("Seven of Clubs", 7),
            new Card("Eight of Clubs", 8),
            new Card("Nine of Clubs", 9),
            new Card("Ten of Clubs", 10),
            new Card("Jack of Clubs", 10),
            new Card("Queen of Clubs", 10),
            new Card("King of Clubs", 10),
            new Card("Ace of Clubs", 11, false),

            new Card("Two of Diamonds", 2),
            new Card("Three of Diamonds", 3),
            new Card("Four of Diamonds", 4),
            new Card("Five of Diamonds", 5),
            new Card("Six of Diamonds", 6),
            new Card("Seven of Diamonds", 7),
            new Card("Eight of Diamonds", 8),
            new Card("Nine of Diamonds", 9),
            new Card("Ten of Diamonds", 10),
            new Card("Jack of Diamonds", 10),
            new Card("Queen of Diamonds", 10),
            new Card("King of Diamonds", 10),
            new Card("Ace of Diamonds", 11, false)
        };

        #endregion

        #region Methods
        public Card GetRandomCardFromDeckAndRemoveCardPicked()
        {
            Random r = new();
            int rndInt = r.Next(0, _deck.Count);
            Card c = _deck[rndInt];
            _deck.RemoveAt(rndInt);
            return c;
        }

        public override string ToString()
        {
            return _deck.ToString();
        }

        #endregion
    }
}
