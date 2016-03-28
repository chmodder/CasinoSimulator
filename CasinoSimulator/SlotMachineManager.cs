using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CasinoSimulator
{
    // This class manages a simulation of the slot machine.
    // Its main responsibility is to communicate with the player,
    // and keep the game running as long as the player wants
    class SlotMachineManager
    {
        // Only one instance variable; a slot machine simulator
        //private SlotMachineSimulator theSimulator;

        public SlotMachineManager()
        {
            //theSimulator = new SlotMachineSimulator();
        }

        #region methods

        //Asks player if Silent mode (log mode) should be enabled
        public bool AskIfSilentModeShouldBeEnabled()
        {
            // Ask the player if (s)he wants to enable silent mode.
            // The only acceptable answer is to press either "y" or "n"
            Console.Write("Enable Silent mode? (press y for Yes, n for No) : ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            // keyStr now holds the players response
            String keyStr = key.KeyChar.ToString();

            if (keyStr == "y")
            {
                // Player pressed "y", so Silent mode is enabled
                return true;
            }
            else if (keyStr == "n")
            {
                // Player pressed "n", so Normal mode is enabled
                return false;
            }
            else
            {
                // Player entered something else than "y" or "n",
                // so ask the user again...
                Console.WriteLine("Please enter either y or n");
                return AskIfSilentModeShouldBeEnabled();
            }
        }


        // This method runs the slot machine simulation.
        // As long as the player wants to play (and has money left),
        // a new spin on the slot machine is done
        public void RunSimulation()
        {
            bool silentMode = false;

            SlotMachineSimulator theSimulator = new SlotMachineSimulator();

            // Reset the simulator, so we start from the initial state
            //theSimulator.Reset();

            Console.WriteLine("Slot Machine Simulator Starting...");
            Console.WriteLine();

            // Print the winning table, for the players convenience
            theSimulator.PrintWinningTable();

            // User adds credits to play for
            int credits = AskPlayerToEnterANumber("Enter credit amount");
            theSimulator.AddCredits(credits);

            Console.WriteLine("You get {0} credits to play for...", credits);
            Console.WriteLine();

            // The main loop: while the player still wants to play - and
            // has money left - a new spin is performed
            #region original code
            //bool playAgain = true;
            //while (playAgain && (theSimulator.GetCredits() > 0))
            //{
            //    // Do another spin on the machine
            //    theSimulator.Spin();

            //    if (theSimulator.GetCredits() > 0)
            //    {
            //        // The player has money left, so ask the player
            //        // if (s)he wants to play again
            //        playAgain = AskPlayerToPlayOrQuit();
            //    }
            //    else
            //    {
            //        // Player has no money left, so the game must stop
            //        Console.WriteLine("Sorry, you have no money left...");
            //        Console.WriteLine();
            //        playAgain = false;
            //    }
            //}
            #endregion
            //Test if 'while' loop can be converted to 'for' loop
            int playAgain = 1;
            for (int i = playAgain; i > 0;)
            {
                if (theSimulator.GetCredits() <= 10)
                {
                    string lowCreditWarning = "WARNING! You have less than 10 credits left!";
                    Console.WriteLine(lowCreditWarning);
                }

                if (i == 1 && (theSimulator.GetCredits() > 0))
                {
                    // Do another spin on the machine
                    theSimulator.Spin(silentMode);

                    if (theSimulator.GetCredits() > 0)
                    {
                        // The player has money left, so ask the player
                        // if (s)he wants to play again

                        i = AskPlayerToPlayOrQuit() ? 1 : 0;
                    }
                }
                else
                {
                    // Player has no money left, so the game must stop
                    Console.WriteLine("Sorry, you have no money left...");
                    Console.WriteLine();
                    i = 0;
                }
            }

            Console.WriteLine("Slot Machine Simulator Ending...");
            Console.WriteLine();
        }

        // This method asks the player if (s)he wants to play again.
        // If the method returns true, the player wants to play again.
        // Otherwise, the player wants to quit.
        // You do not need to understand the details of this method...
        private bool AskPlayerToPlayOrQuit()
        {
            // Ask the player if (s)he wants to play again.
            // The only acceptable answer is to press either "y" or "n"
            Console.Write("Play again? (press y for Yes, n for No) : ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            // keyStr now holds the players response
            String keyStr = key.KeyChar.ToString();

            if (keyStr == "y")
            {
                // Player pressed "y", so we play again
                return true;
            }
            else if (keyStr == "n")
            {
                // Player pressed "n", so we do NOT play again
                return false;
            }
            else
            {
                // Player entered something else than "y" or "n",
                // so ask the user again...
                Console.WriteLine("Please enter either y or n");
                return AskPlayerToPlayOrQuit();
            }
        }

        // This method can be used to get a number from the player.
        // The text given as parameter is displayed to the player.
        // The method will keep asking until the player enters a number.
        // You do not need to understand the details of this method...
        private int AskPlayerToEnterANumber(string textToShowUser)
        {
            Console.WriteLine();
            Console.Write(textToShowUser + ": ");
            string answer = Console.ReadLine();

            int number = 0;
            try
            {
                number = Int32.Parse(answer);
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter a valid number...");
                AskPlayerToEnterANumber(textToShowUser);
            }

            return number;
        }
        #endregion

        public void RunLongSimulation(int noOfCredits, int noOfSpins, bool silentMode)
        {
            SlotMachineSimulator theSimulator = new SlotMachineSimulator();

            //Creates gamelog
            SlotMachineLog gameLog = new SlotMachineLog();

            string startMsg = "Slot Machine Simulator Starting...";
            Console.WriteLine(startMsg);
            Console.WriteLine();

            // Print the winning table, for the players convenience
            theSimulator.PrintWinningTable();

            // User adds credits to play for
            int credits = noOfCredits;
            theSimulator.AddCredits(credits);

            Console.WriteLine("You get {0} credits to play for...", credits);
            Console.WriteLine();

            // The main loop: while the player still wants to play - and
            // has money left - a new spin is performed
            #region original code
            //bool playAgain = true;
            //while (playAgain && (theSimulator.GetCredits() > 0))
            //{
            //    // Do another spin on the machine
            //    theSimulator.Spin();

            //    if (theSimulator.GetCredits() > 0)
            //    {
            //        // The player has money left, so ask the player
            //        // if (s)he wants to play again
            //        playAgain = AskPlayerToPlayOrQuit();
            //    }
            //    else
            //    {
            //        // Player has no money left, so the game must stop
            //        Console.WriteLine("Sorry, you have no money left...");
            //        Console.WriteLine();
            //        playAgain = false;
            //    }
            //}
            #endregion
            //Test if 'while' loop can be converted to 'for' loop
            int playAgain = noOfSpins;
            int spinCounter = 1;
            for (int i = playAgain; i > 0;)
            {
                //Warns user if credit is lower than 10 and not 0
                if (theSimulator.GetCredits() <= 10 && theSimulator.GetCredits() != 0)
                {
                    string lowCreditWarning = "WARNING! You have less than 10 credits left!";
                    if (silentMode)
                    {
                        gameLog.Save(lowCreditWarning);
                    }
                    else
                    {
                        Console.WriteLine(lowCreditWarning);
                    }
                }

                //If player has credit left
                if (theSimulator.GetCredits() > 0)
                {
                    // Do another spin on the machine
                    i--;
                    gameLog.Save("Spincount: " + spinCounter);

                    SlotMachineLog spinLog = theSimulator.Spin(silentMode);

                    List<string> spinLogList = spinLog.GetAllLogItems();
                    spinCounter++;

                    foreach (string s in spinLogList)
                    {
                        gameLog.Save(s);
                    }

                    if (i == 0 && theSimulator.GetCredits() > 0)
                    {
                        Console.Write("Autospin stopped, ");
                        i = AskPlayerToPlayOrQuit() ? AskPlayerToEnterANumber("Enter autospin number") : 0;
                    }
                }
                else
                {
                    // Player has no money left, so the game must stop
                    Console.WriteLine("Sorry, you have no money left...");
                    Console.WriteLine();
                    i = 0;

                    if (AskPlayerYesOrNoQuestion("Print Gamelog"))
                    {
                        gameLog.PrintEntireGameLog();
                    }
                }
            }

            Console.WriteLine("Slot Machine Simulator Ending...");
            Console.WriteLine();
        }

        private bool AskPlayerYesOrNoQuestion(string question)
        {
            // Ask the player a question (input parameter is string).
            // The only acceptable answer is to press either "y" or "n"
            Console.Write("{0}? (press y for Yes, n for No) : ", question);
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            // keyStr now holds the players response
            String keyStr = key.KeyChar.ToString();

            if (keyStr == "y")
            {
                // Player pressed "y"
                return true;
            }
            else if (keyStr == "n")
            {
                // Player pressed "n"
                return false;
            }
            else
            {
                // Player entered something else than "y" or "n",
                // so ask the user again...
                Console.WriteLine("Please enter either y or n");
                return AskPlayerYesOrNoQuestion(question);
            }
        }
    }
}

