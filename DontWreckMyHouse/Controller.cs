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
            view.DisplayHeader(MainMenuOption.AddReservation.ToLabel());
            string hostEmail = consoleIO.ReadRequiredString("Please Enter Host's email Address: ");
            string guestEmail = consoleIO.ReadRequiredString("Please Enter Guest's email Address: ");
            List<Reservation> reservations = reservationService.GetReservationsByEmail(hostEmail);
            view.DisplayHostReservations(reservations);
           
            DateTime startDate = consoleIO.ReadDate("Start  (MM/dd/yyyy): " );
            DateTime endDate = consoleIO.ReadDate("End  (MM/dd/yyyy): ");
            Result<Reservation> result = reservationService.AddGuestReservation(hostEmail, guestEmail, startDate, endDate);
            view.DisplayHeader("Summary");
            view.DisplayHeader("=========");
            if(result != null)
            {
                consoleIO.PrintLine("Hooray, reservation made!");
            }
            else
            {
                consoleIO.PrintLine("Boo! reservation not made!");
            }



        }
        //  Result<Reservation> reservation = ReservationService.Add(Reservation);
    
        private void UpdateReservation()
        {
        
        }
        private void DeleteReservation()
        {

        }
    }

}
