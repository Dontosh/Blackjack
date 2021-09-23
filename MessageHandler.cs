using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
            messages.Add("Welcome", "Welcome to blackjack!\nPress [ENTER] to start. (Press any other key to exit)");
            messages.Add("HitOrStay", "Hit or stay? (h/s) \n");
            messages.Add("PlayAgain", "Play again? (y/n) \n");
            messages.Add("DealerWon", "The dealer won! \n");
            messages.Add("PlayerWon", "Player won! \n");
            messages.Add("Draw", "It's a draw! \n");
            messages.Add("Blackjack", "### BLACKJACK! ### \n");
            messages.Add("PlayerBust", "You bust!\n");
            messages.Add("DealerBust", "The dealer bust!\n");
            messages.Add("PlaySoftAce", "Play soft ace? (y/n)");
            messages.Add("BothBust", "You both bust!");
            messages.Add("InvalidInput", "Invalid input, try again");
            Console.WriteLine(messages[chooseMessage]);
        }

        public static void PrintCards(Person person, List<Card> cardsToPrint)
        {
            
            // This if statement hides all dealer cards except one
            if (person.GameRole.Contains("Dealer") && !DealerFirstShow)
            {
                Console.WriteLine($"{person.GameRole} cards:");
                Console.WriteLine($"\t{person.CardsOnHand[1]}");
                DealerFirstShow = true;
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
            Console.WriteLine("Dealer is playing their cards");
            Thread.Sleep(500);
            Game.ClearConsole();
            Console.WriteLine("Dealer is playing their cards.");
            Thread.Sleep(500);
            Game.ClearConsole();
            Console.WriteLine("Dealer is playing their cards..");
            Thread.Sleep(500);
            Game.ClearConsole();
            Console.WriteLine("Dealer is playing their cards...");
            Thread.Sleep(500);
            Game.ClearConsole();
        }

        #endregion
    }
}