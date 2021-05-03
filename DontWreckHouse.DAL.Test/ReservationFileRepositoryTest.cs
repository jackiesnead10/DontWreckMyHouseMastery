using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckHouse.BLL;
using DontWreckHouse.Core;
using DontWreckHouse.Core.Models;
using NUnit.Framework;

namespace DontWreckHouse.DAL.Test
{
    public class ReservationRepoGuests
    {
        ReservationFileRepository reservrepo;
        [SetUp]
        public void Setup()
        {
            ILogger logger = new FileLogger(@"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckMyHouse.UI\dont-wreck-my-house-data\Logs");

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationsFileDirectory = Path.Combine(projectDirectory, "Data");

            reservrepo = new ReservationFileRepository(reservationsFileDirectory, logger);
        }

        [Test]
        public void ShouldViewRservationsByHost()
        {
            Host host = new Host();
            host.Id = Guid.Parse("3edda6bc-ab95-49a8-8962-d50b53f84b15");
            List <Reservation> result = reservrepo.ViewReservationsByHost(host);

            Assert.AreEqual(result.Count, 13);
        }
        [Test]
        public void ShouldNotViewWhenEmpty()
        {
            Host host = new Host();
            host.Id = Guid.Parse("86bed322-2c2f-4254-8ea3-4821e90c6809");
            List<Reservation> result = reservrepo.ViewReservationsByHost(host);

            Assert.IsEmpty(result);
        
        }
    }
}
