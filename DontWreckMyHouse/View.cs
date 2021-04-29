﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckHouse.Core.Models;

namespace DontWreckMyHouse.UI
{
    class View
    {
        private readonly ConsoleIO io;
        public View(ConsoleIO io)
        {
            this.io = io;
        }

        public MainMenuOption SelectMainMenuOption()
        {
            DisplayHeader("Main Menu");
            int min = int.MaxValue;
            int max = int.MinValue;
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            for (int i = 0; i < options.Length; i++)
            {
                MainMenuOption option = options[i];
                if (!option.IsHidden())
                {
                    io.PrintLine($"{i}. {option.ToLabel()}");
                }

                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max - 1}]: ";
            return options[io.ReadInt(message, min, max)];
        }
        //for dispplating
        public void DisplayHeader(string message)
        {
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
        }

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
        }

        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }
        public string GetHostEmail()
        {
            DisplayHeader(MainMenuOption.ViewReservationsForHost.ToLabel());
            return io.ReadRequiredString("Host Email:");

        }
        public void DisplayHostReservations(List<Reservation> reservations)
        {
            if (reservations == null || reservations.Count == 0)
            {
                io.PrintLine("No reservations found.");
                return;
            }

            foreach (Reservation res in reservations)
            {
                io.PrintLine(
                    string.Format("{0} {1} - {2}:{3} - Value: ${4:0.00}",
                        res.Id,
                        res.StartDate,
                        res.EndDate,
                        res.GuestId,
                        res.Total)
                );
            }
                 
        }
        public void DisplayGuestSummary()
        {
            DisplayHeader("Summary");
            DisplayHeader("=========");

        }
        /*
        public void GetHostDate()
        {
            DisplayHeader(MainMenuOption.AddReservation.ToLabel());
            DisplayHeader("Summary");
            DisplayHeader("=========");
            return io.ReadDate("Start: ");
        }
        */
        
   
 
    }
}
