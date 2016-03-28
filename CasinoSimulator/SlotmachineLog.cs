using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoSimulator
{
    class SlotMachineLog
    {
        private List<string> _log;

        public SlotMachineLog()
        {
            _log = new List<string>();
        }

        // Save a single string
        public void Save(string message)
        {
            _log.Add(message);
        }

        // Print out all the strings saved in the log
        public void PrintEntireGameLog()
        {
            Console.WriteLine("Game log :");
            Console.WriteLine("======================================");
            foreach (string s in _log)
            {
                Console.WriteLine(s);
            }
        }

        public List<string> GetAllLogItems()
        {
            return _log;
        } 

        // Clear out all strings from the log
        public void Reset()
        {
            _log.Clear();
        }

    }
}
