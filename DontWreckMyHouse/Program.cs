using DontWreckMyHouse.UI;
using DontWreckHouse.BLL;
using DontWreckHouse.Core;
using DontWreckHouse.DAL;

using System;
using System.IO;

namespace DontWreckMyHouse
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO io = new ConsoleIO();
            View view = new View(io);
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationsFileDirectory = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "reservations");
            string guestsFilePath = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "guests.csv");
            string hostsFilePath = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "hosts.csv");


            Controller controller = new Controller ( view, io, new GuestFileRepository(guestsFilePath), new HostFileRepository(hostsFilePath), new ReservationFileRepository(reservationsFileDirectory));
            controller.Run();
        }
    }
}
