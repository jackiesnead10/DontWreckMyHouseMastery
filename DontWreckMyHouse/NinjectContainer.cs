using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.IO;
using DontWreckHouse.Core;
using DontWreckHouse.DAL;

namespace DontWreckMyHouse.UI
{
    class NinjectContainer
    {
        public static StandardKernel Kernal { get; private set; }

        public static void Configure()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationsFileDirectory = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "reservations");
            string guestsFilePath = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "guests.csv");
            string hostsFilePath = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "hosts.csv");
            string fileLoggerPath = Path.Combine(projectDirectory, "dont-wreck-my-house-data", "Logs");

            Kernal = new StandardKernel();
            Kernal.Bind<IReservationsFileRepository>().To<ReservationFileRepository>().WithConstructorArgument("filePath", reservationsFileDirectory);
            Kernal.Bind<IGuestFileRepository>().To<GuestFileRepository>().WithConstructorArgument("filePath", guestsFilePath);
            Kernal.Bind<IHostFileRepository>().To<HostFileRepository>().WithConstructorArgument("filePath", hostsFilePath);
            Kernal.Bind<ILogger>().To<FileLogger>().WithConstructorArgument("filepath", Path.Combine(fileLoggerPath, "Logs"));

        }
    }
}
