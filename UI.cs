using System;
using Console = System.Console;

namespace Blackjack_v3
{
    class UI
    {
        #region Fields

        public bool Hit;
        public bool Stay;
        public bool PlayAgain;
        public bool PlaySoftAce;
        public bool WrongInput;

        #endregion

        #region Methods
        public void HandleIo()
        {
            WrongInput = false;
            string userInput = Console.ReadLine();
            
                switch (userInput)
                {
                    case "h":
                        Hit = true;
                        break;
                    case "s":
                        Stay = true;
                        break;
                    case "y":
                        PlayAgain = true;
                        break;
                    case "n":
                        Console.WriteLine("Bye bye, thanks for playing!");
                        Environment.Exit(42);
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again");
                        WrongInput = true;
                        break;
                }

                if (WrongInput)
                {
                    HandleIo();
                }
        }

        public void HandleSoftAce()
        {
            WrongInput = false;
            String userInput = Console.ReadLine();

            switch (userInput)
            {
                case "y":
                    PlaySoftAce = true;
                    break;
                case "n":
                    PlaySoftAce = false;
                    break;
                default:
                    WrongInput = true;
                    break;
            }

            if (WrongInput)
            {
                MessageHandler.MessageChooser("PlaySoftAce");
                HandleSoftAce();
            }
        }

        #endregion
    }
}