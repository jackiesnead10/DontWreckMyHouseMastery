using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckHouse.Core.Models;

namespace DontWreckMyHouse.UI
{
    class ConsoleIO
    {
        private const string INVALID_NUMBER
          = "[INVALID] Enter a valid number.";
        private const string NUMBER_OUT_OF_RANGE
            = "[INVALID] Enter a number between {0} and {1}.";
        private const string REQUIRED
            = "[INVALID] Value is required.";
        private const string INVALID_DATE
            = "[INVALID] Enter a date in MM/dd/yyyy format.";
        private const string INVALID_BOOL
            = "[INVALID] Please enter 'y' or 'n'.";

        public int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                int result = ReadInt(prompt);
                if (result >= min && result <= max)
                {
                    return result;
                }
                PrintLine(string.Format(NUMBER_OUT_OF_RANGE, min, max));
            }
        }
        public void Print(string message)
        {
            Console.Write(message);
        }

        public void PrintLine(string message)
        {
            Console.WriteLine(message);
        }
        public int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                if (int.TryParse(ReadRequiredString(prompt), out result))
                {
                    return result;
                }

                PrintLine(INVALID_NUMBER);
            }
        }
        public string ReadRequiredString(string prompt)
        {
            while (true)
            {
                string result = ReadString(prompt);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    return result;
                }
                PrintLine(REQUIRED);
            }
        }
        public DateTime ReadDate(string prompt)
        {
            DateTime result;
            while (true)
            {
                string input = ReadRequiredString(prompt);
                if (DateTime.TryParse(input, out result))
                {
                    return result.Date;
                }
                PrintLine(INVALID_DATE);
            }
        }
        public string ReadString(string prompt)
        {
            Host host = new Host();
            string response;
            do
            {
                Print(prompt);
                response = Console.ReadLine();

            } while (string.IsNullOrEmpty(response.Trim()) && System.Text.RegularExpressions.Regex.IsMatch(response, "\\d+"));


            return response;
        }

    }
}
