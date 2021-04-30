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
        public Controller (View view, ConsoleIO consoleIO, IGuestFileRepository guestFileRepository, IHostFileRepository hostFileRepository, IReservationsFileRepository reservationsFileRepository)
        {
            this.view = view;
            this.consoleIO = consoleIO;
            this.reservationService = new ReservationService(reservationsFileRepository, guestFileRepository, hostFileRepository);
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
            Host host = new Host();
            Guest guest = new Guest();
            do
            {
                string hostEmail = consoleIO.ReadRequiredString("Please Enter Host's email Address: ");
                host = reservationService.FindByEmail(hostEmail);
                if (host == null)
                {
                    view.DisplayHeader("Host not found. Please enter valid email address.");
                }
                string guestEmail = consoleIO.ReadRequiredString("Please Enter Guest's email Address: ");
                guest = reservationService.FindGuestByEmail(guestEmail);

                if (guest == null)
                {
                    view.DisplayHeader("Guest not found. Please enter valid email address.");
                }
            } while (host == null || guest == null);
            List<Reservation> reservations = reservationService.GetReservationsByEmail(host.Email);
            view.DisplayHostReservations(reservations);

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            do
            {
                startDate = consoleIO.ReadDate("Start  (MM/dd/yyyy): ");
                endDate = consoleIO.ReadDate("End  (MM/dd/yyyy): ");
                if (startDate > endDate)
                {
                    view.DisplayHeader("End date cannot come before start date");
                }
            } while (startDate > endDate);
            decimal total = reservationService.FindTotalCost(host, startDate, endDate);
            
            view.DisplayHeader("Summary");
           // view.DisplayHeader("=========");
            view.DisplayHeader("Start: " + startDate);
            view.DisplayHeader("End: " + endDate);
            view.DisplayHeader("Total " + total);
            bool choice = consoleIO.ReadBool("Is this okay? [y/n] : ");
           // result = reservationService.FindTotalCost();
            if(choice == true)
            {
                consoleIO.PrintLine("Making Reservation....");
                Result<Reservation> result = reservationService.AddGuestReservation(host, guest, startDate, endDate, total);
            }
            else
            {
                consoleIO.PrintLine("Returning to the Main Menu.");
            }



        }
        //  Result<Reservation> reservation = ReservationService.Add(Reservation);
        //update reservation
    
        private void UpdateReservation()
        {
        
        }
        private void DeleteReservation()
        {

        }
    }

}
