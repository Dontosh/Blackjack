using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Blackjack_v3
{
    class MessageHandler
    {
        #region Fields

        Game _game = new();
        public static bool DealerFirstShow;
        public static bool HasUsedSoftAce;

        #endregion

        #region Methods
        public static void MessageChooser(string chooseMessage)
        {
            IDictionary<string, string> messages = new Dictionary<string, string>();
            messages.Add("Welcome", "Welcome to blackjack!\nPress [ENTER] to start.\n\nNote: When you hit and your hand contains an ace,\nthe ace's value is automatically set to a soft ace value if your total exceeds 21.");
            messages.Add("HitOrStay", "\nHit or stay? (h/s) \n");
            messages.Add("PlayAgain", "Play again? (y/n) \n");
            messages.Add("DealerWon", "The dealer won! \n");
            messages.Add("PlayerWon", "Player won! \n");
            messages.Add("PlayerLost", "You lost!\n");
            messages.Add("Draw", "It's a draw! \n");
            messages.Add("Blackjack", "\n### BLACKJACK! ### \n");
            messages.Add("PlayerBust", "\nYou bust!\n");
            messages.Add("DealerBust", "\nThe dealer bust!\n");
            messages.Add("PlaySoftAce", "Play soft ace? (y/n)\n");
            messages.Add("BothBust", "\nYou both bust!\n");
            messages.Add("InvalidInput", "Invalid input, try again\n");
            messages.Add("Push", "PUSH!\n");
            Console.WriteLine(messages[chooseMessage]);
        }

        public static void PrintCards(Person person, List<Card> cardsToPrint)
        {

            // This if statement hides all dealer cards except one
            if (person.GameRole.Contains("Dealer"))
            {
                Console.WriteLine($"{person.GameRole} cards:");
                Console.WriteLine($"\t{person.CardsOnHand[1]}");
                Console.WriteLine("\t..hole card");
            }
            else
            {
                Console.WriteLine($"{person.GameRole} cards:");

                int total = 0;
                int softAceTotal = total - 10;
                foreach (var card in cardsToPrint)
                {
                    if (Game.SoftAceCheck(cardsToPrint, false))
                    {
                        Console.WriteLine($"\t{card}");
                        softAceTotal += card.Value;
                    }
                    else
                    {
                        Console.WriteLine($"\t{card}");
                    }
                    total += card.Value;
                }

                // Prints the players total
                if (softAceTotal > 0 && !HasUsedSoftAce)
                {
                    Console.WriteLine($"\tTotal: {total} or {softAceTotal}");
                }
                else if (softAceTotal > 0 && HasUsedSoftAce)
                {
                    Console.WriteLine($"\tTotal: {total}");
                }
                else
                {
                    Console.WriteLine($"\tTotal: {total}");
                }
                
            }
        }

        public static void ShowCardsAtEndOfGame(Person person)
        {
            int total = 0;
            Console.WriteLine($"{person.GameRole} cards:");
            foreach (Card card in person.CardsOnHand)
            {
                Console.WriteLine($"{card}");
                total += card.Value;
            }

            Console.WriteLine($"\n\tTotal: {total}\n");
        }

        public static void DealerIsPlayingMessage()
        {
            Game.ClearConsole();
            Console.WriteLine("The dealer is playing...");
            Thread.Sleep(300);
            Game.ClearConsole();
            Console.WriteLine("The dealer is playing...");
            Thread.Sleep(300);
            Game.ClearConsole();
            Console.WriteLine("The dealer is playing...");
            Thread.Sleep(300);
            Game.ClearConsole();
        }

        public static void DealerRevealsHoleCard(Person player, Person dealer)
        {
            int total = 0;
            Game.EmptyLine(4 + player.CardsOnHand.Count);

            Console.SetCursorPosition(0, 4 + player.CardsOnHand.Count);
            Console.WriteLine("\tFlipping hole card.");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 4 + player.CardsOnHand.Count);
            Console.WriteLine("\tFlipping hole card..");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 4 + player.CardsOnHand.Count);
            Console.WriteLine("\tFlipping hole card...");
            Thread.Sleep(500);
            Game.EmptyLine(4 + player.CardsOnHand.Count);
            Console.SetCursorPosition(0, 4 + player.CardsOnHand.Count);
            Console.WriteLine("\t" + dealer.CardsOnHand[0]);
            Thread.Sleep(500);
            Console.SetCursorPosition(0, player.CardsOnHand.Count + dealer.CardsOnHand.Count);
            foreach (Card card in dealer.CardsOnHand)
            {
                total += card.Value;
            }
            Console.SetCursorPosition(0,  3 + player.CardsOnHand.Count + dealer.CardsOnHand.Count);
            Console.WriteLine($"\tTotal: {total}");
        }
        public static void DealerDrawsCards(Person dealer, Person player)
        {
            int total = 0;
            Thread.Sleep(2000);
            Game.EmptyLine(2 + player.CardsOnHand.Count + dealer.CardsOnHand.Count);
            Console.SetCursorPosition(0, 2 + player.CardsOnHand.Count + dealer.CardsOnHand.Count);
            Console.WriteLine("\tDrawing.");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 2 + player.CardsOnHand.Count + dealer.CardsOnHand.Count);
            Console.WriteLine("\tDrawing..");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 2 + player.CardsOnHand.Count + dealer.CardsOnHand.Count);
            Console.WriteLine("\tDrawing...");
            Thread.Sleep(500);
            Game.EmptyLine(2 + player.CardsOnHand.Count);
            Console.SetCursorPosition(0, 2 + player.CardsOnHand.Count + dealer.CardsOnHand.Count);
            Console.WriteLine($"\t{dealer.CardsOnHand.Last().Type}, {dealer.CardsOnHand.Last().Value}");
            Console.SetCursorPosition(0, 3 + player.CardsOnHand.Count + dealer.CardsOnHand.Count);

            foreach (Card card in dealer.CardsOnHand)
            {
                total += card.Value;
            }

            Console.WriteLine($"\tTotal: {total}");
        }

        #endregion
    }
}