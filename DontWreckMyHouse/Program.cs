using DontWreckMyHouse.UI;
using DontWreckHouse.BLL;
using DontWreckHouse.Core;
using DontWreckHouse.DAL;
using Ninject;
using System;
using System.IO;

namespace DontWreckMyHouse
{
    public class Program
    {
        static void Main(string[] args)
        {
           ConsoleIO io = new ConsoleIO();
          // GuestFileRepository guestFileRepository = new GuestFileRepository();
          // ReservationService reservationService = new ReservationService();
          // View view = new View(io, reservationService);

            NinjectContainer.Configure();
            /*
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationsFileDirectory = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "reservations");
            string guestsFilePath = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "guests.csv");
            string hostsFilePath = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "hosts.csv");
            */
            var controller = NinjectContainer.Kernal.Get<Controller>();
          // Controller controller = new Controller (new GuestFileRepository(guestsFilePath), new HostFileRepository(hostsFilePath), new ReservationFileRepository(reservationsFileDirectory));
            controller.Run();
        }
    }
}
