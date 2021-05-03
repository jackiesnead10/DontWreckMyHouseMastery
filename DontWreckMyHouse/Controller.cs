using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckHouse.Core;
using DontWreckHouse.Core.exceptions;
using DontWreckHouse.Core.Models;
using DontWreckHouse.BLL;

namespace DontWreckMyHouse.UI
{
    class Controller
    {
        private readonly View view;
        private ConsoleIO consoleIO;
        private readonly ReservationService reservationService;
        public Controller (IGuestFileRepository guestFileRepository, IHostFileRepository hostFileRepository, IReservationsFileRepository reservationsFileRepository, ILogger fileLogger)
        {
            this.consoleIO = new ConsoleIO();
            this.reservationService = new ReservationService(reservationsFileRepository, guestFileRepository, hostFileRepository, fileLogger);
            this.view = new View(consoleIO, reservationService);
        }

        public void Run()
        {
            view.DisplayHeader("Don't Wreck My House Menu");
            try
            {
                RunAppLoop();
            }
            catch (RepositoryException ex)
            {
                view.DisplayException(ex);
            }
            view.DisplayHeader("Goodbye.");
        
        
        }
        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch (option)
                {
                    case MainMenuOption.ViewReservationsForHost:
                        ViewReservationForHost();
                        break;
                    case MainMenuOption.AddReservation:
                        AddReservation();
                        break;
                    case MainMenuOption.UpdateReservation:
                        UpdateReservation();
                        break;
                    case MainMenuOption.DeleteReservation:
                        DeleteReservation();
                        break;
                }
            } while (option != MainMenuOption.Exit);
        }
        private void ViewReservationForHost()
        {
            string email = consoleIO.ReadRequiredString("Please Enter Host's email Address: ");
            List<Reservation> reservations = reservationService.GetReservationsByEmail(email);
            view.DisplayHostReservations(reservations);
            view.EnterToContinue();
        }
        private void AddReservation()
        {


            //FIX THE VALIDATION, should probably be in consoleIO
            view.DisplayHeader(MainMenuOption.AddReservation.ToLabel());
            Host host = consoleIO.GetHost(reservationService);
            Guest guest = consoleIO.GetGuest(reservationService);
            
            List<Reservation> reservations = reservationService.GetReservationsByEmail(host.Email);
            view.DisplayHostReservations(reservations);

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            do
            {
                startDate = consoleIO.ReadDate("Start  (MM/dd/yyyy): ");
                endDate = consoleIO.ReadDate("End  (MM/dd/yyyy): ");
                if (startDate >= endDate)
                {
                    view.DisplayHeader("End date cannot come before, or be the start date");
                }
            } while (startDate >= endDate);
            decimal total = reservationService.FindTotalCost(host, startDate, endDate);
            
            view.DisplayHeader("Summary");
           
            view.DisplayHeader("Start: " + startDate);
            view.DisplayHeader("End: " + endDate);
            view.DisplayHeader("Total " + total);
            bool choice = consoleIO.ReadBool("Is this okay? [y/n] : ");
          
            if(choice == true)
            {
                consoleIO.PrintLine("Making Reservation....");
                Result<Reservation> result = reservationService.AddGuestReservation(host, guest, startDate, endDate, total);

                if(result.Messages.Count > 0)
                {
                    consoleIO.PrintLine("Error: Time slot is occupied by another guest, please try again with another time slot.");
                }
                else
                {
                    consoleIO.PrintLine("Success! Reservation has been made.");
                }
            }
            else
            {
                consoleIO.PrintLine("Returning to the Main Menu.");
            }



        }
        
        //update reservation
    
        private void UpdateReservation()
        {
            view.DisplayHeader("Edit a reservation");

            Guest guest = consoleIO.GetGuest(reservationService);
            Host host = consoleIO.GetHost(reservationService);
            List<Reservation> reservations = reservationService.GetReservationsByEmail(host.Email);
            reservations = reservationService.HostHasGuestReservation(guest, reservations);

            if(reservations.Count == 0)
            {
                consoleIO.PrintLine("Error: No reservations found for this guest.");
                return;
            }
            view.PrintGuestReservations(guest, reservations);
            int id = consoleIO.ReadId("Reservation ID Input: ", reservations);
            
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            do
            {
                startDate = consoleIO.ReadDate("Start  (MM/dd/yyyy): ");
                endDate = consoleIO.ReadDate("End  (MM/dd/yyyy): ");
                if (startDate >= endDate)
                {
                    view.DisplayHeader("End date cannot come before, or be the start date");
                }
            } while (startDate >= endDate);
            decimal total = reservationService.FindTotalCost(host, startDate, endDate);
            view.DisplayHeader("Summary");
            view.DisplayHeader("Start: " + startDate);
            view.DisplayHeader("End: " + endDate);
            view.DisplayHeader("Total " + total);
            bool choice = consoleIO.ReadBool("Is this okay? [y/n] : ");
            if (choice == true)
            {
                consoleIO.PrintLine("Updating Reservation....");
                Reservation reservation = new Reservation();
                reservation.Id = id;
                reservation.StartDate = startDate;
                reservation.EndDate = endDate;
                reservation.Total = total;
                Result<Reservation> result = reservationService.UpdateReservation(host, reservation, reservations);
                if (result.success == true)
                {
                    consoleIO.PrintLine("Reservation " + id + " updated!");
                }
                else
                {
                    if (result.Messages.Count > 0)
                    {
                        consoleIO.PrintLine(result.Messages[0]);
                    }
                    else
                    {
                        consoleIO.PrintLine("Reservation could not be found.");
                    }
                }
            }
            else
            {
                consoleIO.PrintLine("Returning to the Main Menu.");
            }

        }
        private void DeleteReservation()
        {
            view.DisplayHeader("Cancel a Reservation");
            
           
            Guest guest = consoleIO.GetGuest(reservationService);
            Host host = consoleIO.GetHost(reservationService);
            List<Reservation> reservations = reservationService.GetReservationsByEmail(host.Email);
            view.PrintGuestReservations(guest, reservations);
            int id = consoleIO.ReadInt("Reservation ID Input: ");
            Result<Reservation> result = reservationService.DeleteReservation(id, host);

            if(result.success == true)
            {
                consoleIO.PrintLine("Reservation " + id + " cancelled.");
            }
            else
            {
                consoleIO.PrintLine("Reservation could not be found.");
            }
        }
    }

}
