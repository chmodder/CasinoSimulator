using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoSimulator
{
    // This class is supposed to simulate a classic slot machine.
    // The slot machine has three "dials"; each dial can show:
    // 7 : "Seven"
    // # : "Sharp"
    // @ : "At"
    //
    // The player can enter "credits" into the machine.
    // Each spin on the machine costs one credit.
    // Certain combinations of the three dials gives the player
    // a winning.
    //
    class SlotMachineSimulator
    {
        #region INSTANCE FIELDS

        // VARIABLES

        // The numbers of credits left in the machine
        private int _credits;

        // The three dials on the slot machine
        private string _dial1;
        private string _dial2;
        private string _dial3;

        // This instance variable is used for generating random numbers
        private Random _generator;


        // CONSTANTS

        // The probabilities that a dial shows a certain symbol:
        // 10 % for showing "7"
        // 30 % for showing "#"
        // 60 % for showing "@"
        //
        private const int ProbabilitySeven = 10;
        private const int ProbabilitySharp = 30;
        private const int ProbabilityAt = 60;

        // The winnings paid to the player for certain dial combinations
        // 7 7 7 pays 150
        // # # # pays 10
        // @ @ @ pays 1
        // any two 7 pays 5
        // any two # pays 1
        //
        private const int PayoutFor3Sevens = 150;
        private const int PayoutFor3Sharps = 10;
        private const int PayoutFor3Ats = 1;
        private const int PayoutFor2Sevens = 5;
        private const int PayoutFor2Sharps = 1;
        #endregion

        #region CONSTRUCTOR
        public SlotMachineSimulator()
        {
            _generator = new Random();
            Reset();
        }
        #endregion

        #region PUBLIC METHODS
        // Sets the simulator back to its starting state
        public void Reset()
        {
            _credits = 0;
        }

        // Returns the number of credits left in the machine
        public int GetCredits()
        {
            return _credits;
        }

        // Adds a number of credits to the machine
        public void AddCredits(int moreCredits)
        {
            _credits = _credits + moreCredits;
        }

        // Perform one spin on the machine
        public SlotMachineLog Spin(bool silentMode)
        {
            SlotMachineLog spinLog = new SlotMachineLog();

            //Informs the player how many credits are left before spinning
            if (silentMode)
            {
                spinLog.Save(GetMessageBeforeSpin());
            }
            else
            {
                PrintMessageBeforeSpin();
            }

            // Spin the dials
            SpinAllDials();

            //Prints outcome of the spin
            if (silentMode)
            {
                foreach (string item in GetOutcomeOfSpin())
                {
                    spinLog.Save(item);
                }
            }
            else
            {
                PrintOutcomeOfSpin();
            }
            return spinLog;
        }

        


        // Print the complete winning table
        public void PrintWinningTable()
        {
            Console.WriteLine("----------- Winning table for slot machine --------------");
            Console.WriteLine(" 7 7 7 pays      {0}", PayoutFor3Sevens);
            Console.WriteLine(" # # # pays      {0}", PayoutFor3Sharps);
            Console.WriteLine(" @ @ @ pays      {0}", PayoutFor3Ats);
            Console.WriteLine(" any two 7 pays  {0}", PayoutFor2Sevens);
            Console.WriteLine(" any two # pays  {0}", PayoutFor2Sharps);
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine();
        }
        #endregion

        #region PRIVATE METHODS

        // Generate a "percentage", i.e. a number between 0 and 100 (100 not included)
        private int GeneratePercentage()
        {
            int result = _generator.Next(100);
            return result;
        }
        //Informs the player how many credits are left before spinning
        private void PrintMessageBeforeSpin()
        {
            // Inform the player how many credits are left before spinning
            Console.WriteLine();
            Console.WriteLine("Credits left : {0}, now spinning...", _credits);
        }
        private string GetMessageBeforeSpin()
        {
            string beforeMsg = "Credits left : " + _credits + ", now spinning...";
            return beforeMsg;
        }
        // Spin the dials
        private void SpinAllDials()
        {
            // One spin costs one credit
            _credits--;

            _dial1 = SpinDial();
            _dial2 = SpinDial();
            _dial3 = SpinDial();
        }

        //Prints outcome of the spin
        private void PrintOutcomeOfSpin()
        {
            // Find the winnings, and update the credits left
            int winnings = CalculateWinnings(_dial1, _dial2, _dial3);
            _credits = _credits + winnings;

            // Report the outcome of the spin to the player
            Console.WriteLine("You got : {0} {1} {2}", _dial1, _dial2, _dial3);

            StringBuilder winningsMsg = new StringBuilder("You won ");
            winningsMsg.Append(winnings);
            winningsMsg.Append(" credits");

            if (winnings == 1)
            {
                Console.WriteLine(winningsMsg);
            }
            else if (winnings > 1)
            {
                Console.WriteLine(winningsMsg.Append(", congratulations!"));
            }
            else
            {
                Console.WriteLine("Sorry, you did not win anything");
            }

            Console.WriteLine("Credits left : {0}", _credits);
        }
        private List<string> GetOutcomeOfSpin()
        {
            List<string> log = new List<string>();

            // Find the winnings, and update the credits left
            int winnings = CalculateWinnings(_dial1, _dial2, _dial3);
            _credits = _credits + winnings;

            // Report the outcome of the spin to the player
            log.Add("You got : " + _dial1 + " " + _dial2 + " " + _dial3);

            StringBuilder winningsMsg = new StringBuilder("You won ");
            winningsMsg.Append(winnings);
            winningsMsg.Append(" credits");

            if (winnings == 1)
            {
                log.Add(winningsMsg.ToString());
            }
            else if (winnings > 1)
            {
                log.Add(winningsMsg.Append(", congratulations!").ToString());
            }
            else
            {
                log.Add("Sorry, you did not win anything");
            }

            log.Add("Credits left : " + _credits);
            log.Add("--------------------------------------");
            
            return log;
        }

        // Spin a dial, using the defined percentages
        private string SpinDial()
        {
            // Generate a random percentage
            int percentage = GeneratePercentage();

            // Given the random percentage, find out what the dial should show
            //
            if (percentage < ProbabilitySeven)
            {
                return "7";
            }
            else if (percentage < (ProbabilitySeven + ProbabilitySharp))
            {
                return "#";
            }
            else
            {
                return "@";
            }
        }

        // Calculate the winnings corresponding to the given dial combination
        private int CalculateWinnings(string dial1, string dial2, string dial3)
        {
            if (CountSymbols("7", dial1, dial2, dial3) == 3)
            {
                return PayoutFor3Sevens;
            }
            else if (CountSymbols("#", dial1, dial2, dial3) == 3)
            {
                return PayoutFor3Sharps;
            }
            else if (CountSymbols("@", dial1, dial2, dial3) == 3)
            {
                return PayoutFor3Ats;
            }
            else if (CountSymbols("7", dial1, dial2, dial3) == 2)
            {
                return PayoutFor2Sevens;
            }
            else if (CountSymbols("#", dial1, dial2, dial3) == 2)
            {
                return PayoutFor2Sharps;
            }
            else
            {
                return 0;
            }
        }

        // A helper method for counting how many of the three strings that
        // are equal to the "target" string
        private int CountSymbols(string target, string c1, string c2, string c3)
        {
            int count = 0;

            if (target == c1) count++;
            if (target == c2) count++;
            if (target == c3) count++;

            return count;
        }
        #endregion
    }
}
