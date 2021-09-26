using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Blackjack_v3
{
    class Game
    {
        #region Fields

        private Person _player = new("Player");
        private Person _dealer = new("Dealer");
        private UI _ui = new();
        public Deck Deck = new();

        #endregion

        #region Methods

        public void Welcome()
        {
            Console.Title = "Blackjack";
            ClearConsole();
            MessageHandler.MessageChooser("Welcome");
            var userKeyInput = Console.ReadKey();
            if (userKeyInput.Key == ConsoleKey.Enter)
            {
                ClearConsole();
                StartGame();
            }
            else
            {
                ClearConsole();
                Console.WriteLine("Bye bye, thanks for playing!");
                Environment.Exit(42);
            }
        }

        public void StartGame()
        {
            ClearConsole();
            MessageHandler.DealerFirstShow = false;
            _player = new("Player");
            _dealer = new("Dealer");
            _ui = new();
            Deck = new();
            DrawCardsAndPrintFirstRound();

            MessageHandler.MessageChooser("HitOrStay");
            _ui.HandleIo();

            if (_ui.Hit)
            {
                _ui.Hit = false;
                //HitMe();
                PlayerHit();
            }
            else if (_ui.Stay)
            {
                _ui.Stay = false;
                Console.WriteLine("Dealers turn");
                DealersTurn();
            }
        }

        public static void ClearConsole()
        {
            Console.Clear();
        }

        public void DrawCardsAndPrintFirstRound()
        {
            _player.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
            _player.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
            _dealer.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
            _dealer.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());

            MessageHandler.PrintCards(_player, _player.CardsOnHand);
            MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
        }


        public void PlayerHit()
        {
            int playerTotal = PlayerTotal(_player.CardsOnHand);

            switch (playerTotal)
            {
                // if player has less than 21, then he can hit
                case < 21:
                    ClearConsole();
                    _player.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
                    playerTotal += _player.CardsOnHand.Last().Value;
                    bool hasSoftAce = SoftAceCheck(_player.CardsOnHand, false);

                    if (playerTotal > 21 && !hasSoftAce)
                    {
                        MessageHandler.MessageChooser("PlayerBust");
                        MessageHandler.PrintCards(_player, _player.CardsOnHand);
                        MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);

                        MessageHandler.MessageChooser("PlayAgain");
                        _ui.HandleIo();
                        if (_ui.PlayAgain)
                        {
                            StartGame();
                        }
                        else
                        {
                            Environment.Exit(42);
                        }
                    } else if (playerTotal > 21 && hasSoftAce)
                    {
                        SoftAceCheck(_player.CardsOnHand, true);
                    }
                    MessageHandler.PrintCards(_player, _player.CardsOnHand);
                    MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
                    MessageHandler.MessageChooser("HitOrStay");
                    _ui.HandleIo();
                    if (_ui.Hit)
                    {
                        _ui.Hit = false;
                        PlayerHit();
                    } else if (_ui.Stay)
                    {
                        _ui.Stay = false;
                        DealersTurn();
                    }
                    break;

                case > 21:
                    if (SoftAceCheck(_player.CardsOnHand, false))
                    {
                        SoftAceCheck(_player.CardsOnHand, true);
                        MessageHandler.PrintCards(_player, _player.CardsOnHand);
                        MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);

                        MessageHandler.MessageChooser("HitOrStay");
                        _ui.HandleIo();
                        if (_ui.Hit)
                        {
                            _ui.Hit = false;
                            PlayerHit();
                        }
                        else if (_ui.Stay)
                        {
                            _ui.Stay = false;
                            DealersTurn();
                        }


                    }
                    MessageHandler.MessageChooser("PlayerBust");
                    MessageHandler.PrintCards(_player, _player.CardsOnHand);
                    MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
                    MessageHandler.MessageChooser("PlayAgain");
                    _ui.HandleIo();
                    if (_ui.PlayAgain)
                    {
                        StartGame();
                    }
                    else
                    {
                        Environment.Exit(42);
                    }

                    break;

                case 21:
                    MessageHandler.MessageChooser("Blackjack");
                    MessageHandler.MessageChooser("PlayerWon");
                    MessageHandler.PrintCards(_player, _player.CardsOnHand);
                    break;
            }
        }

        //public void HitMe()
        //{
        //    ClearConsole();

        //    int playerTotal = PlayerTotal(_player.CardsOnHand);
        //    //if (SoftAceCheck(_player.CardsOnHand, false))
        //    //{
        //        //if (playerTotal == 21)
        //        //{
        //        //    MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //        //    MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
        //        //    MessageHandler.MessageChooser("HitOrStay");
        //        //    _ui.HandleIo();
        //        //    if (_ui.Hit)
        //        //    {
        //        //        _ui.Hit = false;
        //        //        HitMe();
        //        //    } else if (_ui.Stay)
        //        //    {
        //        //        _ui.Stay = false;
        //        //        DealersTurn();
        //        //    }
        //        //}
        //        //MessageHandler.MessageChooser("PlaySoftAce");
        //        //_ui.HandleSoftAce();
        //        //if (_ui.PlaySoftAce)
        //        //{
        //        //    ClearConsole();
        //        //    MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //        //    MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
        //        //    MessageHandler.HasUsedSoftAce = true;
        //        //    SoftAceCheck(_player.CardsOnHand, true);
        //        //}
        //    //    MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //    //}

        //    switch (playerTotal)
        //    {
        //        case > 21:

        //            SoftAceCheck(_player.CardsOnHand, true);
        //            MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //            MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);

        //            MessageHandler.MessageChooser("HitOrStay");
        //            _ui.HandleIo();

        //            if (_ui.Hit)
        //            {
        //                _player.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
        //                _ui.Hit = false;
        //                HitMe();
        //            }
        //            else if (_ui.Stay)
        //            {
        //                _ui.Stay = false;
        //                DealersTurn();
        //            }

        //            //bool hasSoftAce = SoftAceCheck(_player.CardsOnHand, false);
        //            //playerTotal = PlayerTotal(_player.CardsOnHand);
        //            //if (hasSoftAce)
        //            //{
        //            //    MessageHandler.MessageChooser("PlaySoftAce");
        //            //    _ui.HandleSoftAce();
        //            //    if (_ui.PlaySoftAce)
        //            //    {
        //            //        ClearConsole();
        //            //        MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //            //        MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
        //            //        MessageHandler.HasUsedSoftAce = true;
        //            //        SoftAceCheck(_player.CardsOnHand, true);
        //            //        playerTotal += _player.CardsOnHand.Last().Value;
        //            //    }
        //            //}
        //            break;

        //        case < 21:
        //            _player.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
                   

        //            playerTotal = PlayerTotal(_player.CardsOnHand);
        //            if (playerTotal < 21)
        //            {
        //                MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //                MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
        //                MessageHandler.MessageChooser("HitOrStay");
        //                _ui.HandleIo();

        //                if (_ui.Hit)
        //                {
        //                    _ui.Hit = false;
        //                    HitMe();
        //                }
        //                else if (_ui.Stay)
        //                {
        //                    _ui.Stay = false;
        //                    DealersTurn();
        //                }
        //            }
        //            else if (playerTotal > 21)
        //            {
        //                SoftAceCheck(_player.CardsOnHand, true);
        //                playerTotal = PlayerTotal(_player.CardsOnHand);
                        
        //                MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //                MessageHandler.PrintCards(_dealer, _player.CardsOnHand);
        //                MessageHandler.MessageChooser("HitOrStay");
        //                    //MessageHandler.MessageChooser("PlaySoftAce");
        //                    //_ui.HandleSoftAce();
        //                    //if (_ui.PlaySoftAce)
        //                    //{
        //                    //    MessageHandler.HasUsedSoftAce = true;
        //                    //    SoftAceCheck(_player.CardsOnHand, true);
        //                    //    foreach (Card card in _player.CardsOnHand)
        //                    //    {
        //                    //        playerTotal += card.Value;
        //                    //    }
        //                    //}
                        
        //                DealersTurn();
        //            }
        //            //else 
        //            //{
        //            //    MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //            //    MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
        //            //    MessageHandler.MessageChooser("HitOrStay");
        //            //    _ui.HandleIo();
        //            //    if (_ui.Hit)
        //            //    {
        //            //        _ui.Hit = false;
        //            //        HitMe();
        //            //    } else if (_ui.Stay)
        //            //    {
        //            //        _ui.Stay = false;
        //            //        DealersTurn();
        //            //    }
        //            //}

        //            break;

        //        case 21:
        //            SoftAceCheck(_player.CardsOnHand, true);
        //            playerTotal = PlayerTotal(_player.CardsOnHand);

        //            MessageHandler.PrintCards(_player, _player.CardsOnHand);
        //            MessageHandler.PrintCards(_dealer, _dealer.CardsOnHand);
        //            MessageHandler.MessageChooser("HitOrStay");
        //            _ui.HandleIo();
        //            if (_ui.Hit)
        //            {
        //                _player.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
        //                _ui.Hit = false;
        //                HitMe();
        //            }
        //            else if (_ui.Stay)
        //            {
        //                _ui.Stay = false;
        //                DealersTurn();
        //            }
        //            break;
        //    }
        //}

        public void DealersTurn()
        {
            ClearScreenUnderHoleCard();
            MessageHandler.DealerRevealsHoleCard(_player, _dealer);

            int dealerTotal = DealerTotal(_dealer.CardsOnHand);

            while (dealerTotal < 17)
            {
                _dealer.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
                MessageHandler.DealerDrawsCards(_dealer, _player);
                dealerTotal += _dealer.CardsOnHand.Last().Value;
            }

            dealerTotal = DealerTotal(_dealer.CardsOnHand);

            if (dealerTotal > 21)
            {
                var hasSoftAce = SoftAceCheck(_dealer.CardsOnHand, true); // Checks if dealer has a soft ace
                dealerTotal = DealerTotal(_dealer.CardsOnHand);
                
                while (dealerTotal < 17) // If dealer had soft ace, and is still under 17, then dealer must draw another card
                {
                    if (hasSoftAce)
                    {
                        Console.WriteLine("The ace value is now 1");
                    }

                    _dealer.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
                    MessageHandler.DealerDrawsCards(_dealer, _player);
                    dealerTotal += _dealer.CardsOnHand.Last().Value;
                }



                SoftAceCheck(_dealer.CardsOnHand, true);
                

                if (dealerTotal < 21)
                {
                    SoftAceCheck(_dealer.CardsOnHand, true);
                    dealerTotal = DealerTotal(_dealer.CardsOnHand);

                    while (dealerTotal < 17)
                    {
                        _dealer.AddCardToHand(Deck.GetRandomCardFromDeckAndRemoveCardPicked());
                        SoftAceCheck(_dealer.CardsOnHand, true);
                        dealerTotal = DealerTotal(_dealer.CardsOnHand);
                    }
                }

                if (dealerTotal == 21)
                {
                    SoftAceCheck(_dealer.CardsOnHand, true);
                    dealerTotal = DealerTotal(_dealer.CardsOnHand);
                    CalculateResults();
                }

                
            }
            else if (dealerTotal > 21)
            {
                CalculateResults();
            }
            {
                CalculateResults();
            }
            CalculateResults();
        }

        public int DealerTotal(List<Card> dealerCards)
        {
            int dealerTotal = 0;
            foreach (Card card in _dealer.CardsOnHand)
            {
                dealerTotal += card.Value;
            }

            return dealerTotal;
        }

        public int PlayerTotal(List<Card> playerCards)
        {
            int playerTotal = 0;
            foreach (Card card in _player.CardsOnHand)
            {
                playerTotal += card.Value;
            }

            return playerTotal;
        }

        public void CalculateResults()
        {
            Console.WriteLine("Dealer cards: ");
            int dealerTotal = DealerTotal(_dealer.CardsOnHand);
            int playerTotal = PlayerTotal(_player.CardsOnHand);

            if (dealerTotal > 21 && playerTotal < 21)
            {
                MessageHandler.MessageChooser("DealerBust");
                
                PlayAgain();
            }

            if (playerTotal > 21 && dealerTotal < 21)
            {
                MessageHandler.MessageChooser("PlayerBust");
                
                PlayAgain();
            }
            else if (dealerTotal < 21 && playerTotal < 21)
            {
                if (dealerTotal > playerTotal)
                {
                    MessageHandler.MessageChooser("PlayerLost");
                    
                    PlayAgain();
                }
                else if (dealerTotal == playerTotal)
                {
                    MessageHandler.MessageChooser("PlayerWon");
                   
                    PlayAgain();
                }
                else
                {
                    MessageHandler.MessageChooser("PlayerWon");
                    
                    PlayAgain();
                }
            } 
            else if (dealerTotal > 21 && playerTotal > 21)
            {
                MessageHandler.MessageChooser("BothBust");
                
                PlayAgain();
            }
            else if (dealerTotal == 21 && playerTotal == 21)
            {
                MessageHandler.MessageChooser("Push");
                
                PlayAgain();
            }

            else if (dealerTotal == 21 && playerTotal != 21)
            {
                MessageHandler.MessageChooser("Blackjack");
                MessageHandler.MessageChooser("DealerWon");
                
                PlayAgain();
            }
            else if (dealerTotal != 21 && playerTotal == 21)
            {
                MessageHandler.MessageChooser("Blackjack");
                MessageHandler.MessageChooser("PlayerWon");
                PlayAgain();
            }
        }

        public static bool SoftAceCheck(List<Card> cardsToCheckForSoftAce, bool playSoftAce)
        {
            /*
            First if-statement:
                If a card in the deck starts with "Ace" and the player chooses to play the soft ace, and the card has not already been played as a soft ace, play the soft ace
            Second if-statement:
                
             */
            foreach (Card card in cardsToCheckForSoftAce)
            {
                if (card.Type.Substring(0, 3) == "Ace" && playSoftAce && !card.IsSoftAce)
                {
                    card.Value = 1;
                    card.IsSoftAce = true;
                    return true;
                }
                if (card.Type.Substring(0, 3) == "Ace" && !playSoftAce && !card.IsSoftAce)
                {
                    return true;
                }

                if (card.Type.Substring(0, 3) == "Ace" && card.IsSoftAce)
                {
                    return false;
                }
            }

            return false;
        }

        public void PlayAgain()
        {
            MessageHandler.MessageChooser("PlayAgain");
            _ui.HandleIo();
            if (_ui.PlayAgain)
            {
                _ui.PlayAgain = false;
                StartGame();
            }
            else
            {
                MessageHandler.MessageChooser("InvalidInput");
            }
        }

        public static void EmptyLine(int top)
        {
            Console.SetCursorPosition(0, top);
            Console.WriteLine("                                      ");
        }

        public void ClearScreenUnderHoleCard()
        {
            int holeCardPos = 4 + _dealer.CardsOnHand.Count + _player.CardsOnHand.Count;
            Console.SetCursorPosition(0, holeCardPos);
            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
        }

        #endregion 
    }
}