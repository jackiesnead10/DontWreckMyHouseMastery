using NUnit.Framework;
using System;
using DontWreckHouse.Core.Models;
using DontWreckHouse.Core;
using DontWreckHouse.BLL;


namespace DontWreckHouse.BLL.Test
{
    public class Tests
    {


        ReservationService forreservation = new ReservationService(new ReservationRepositoryDouble(), new GuestFileRepositoryDouble(), new HostFileRepositoryDouble(), new FileLogger(@"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckMyHouse.UI\dont-wreck-my-house-data\Logs"));



        [Test]
        public void ShouldAddReservation()
        {
            DateTime StartDate = new DateTime(2021, 02, 02);
            DateTime EndDate = new DateTime(2021, 04, 04);

            Guest guest = new Guest();
            guest.GuestId = 4;
            guest.FirstName = "Good";
            guest.LastName = "bye";
            guest.Email = "goodbye@gmail.com";
            guest.Phone = "1234456780";
            guest.State = "Alaska";


            Host host = new Host();
            host.Id = Guid.Parse("b6ddb844-b990-471a-8c0a-519d0777eb9b");
            host.LastName = "Hello";
            host.Email = "hello@hello.com";
            host.Phone = "(000)000000";
            host.Address = "0000 Nothing Dr";
            host.City = "Ocala";
            host.State = "TX";
            host.PostalCode = "10100";
            host.StandardRate = 100;
            host.WeekendRate = 1000;

            decimal total = 1000.00M;
            Result<Reservation> result = forreservation.AddGuestReservation(host, guest, StartDate, EndDate, total);

            Assert.IsTrue(result.success);
        }
        [Test]
        public void ShouldNotAddDuplicate()
        {
            DateTime StartDate = new  DateTime(2021, 02, 02);
            DateTime EndDate = new DateTime(2021, 04, 04);

            Host host = new Host();
            host.Id = Guid.Parse("b6ddb844-b990-471a-8c0a-519d0777eb9b");
            host.LastName = "Hello";
            host.Email = "hello@hello.com";
            host.Phone = "(000)000000";
            host.Address = "0000 Nothing Dr";
            host.City = "Ocala";
            host.State = "TX";
            host.PostalCode = "10100";
            host.StandardRate = 100;
            host.WeekendRate = 1000;

            Guest guest = new Guest();
            guest.GuestId = 1;
            guest.FirstName = "ILean";
            guest.LastName = "Boat";
            guest.Email = "boat@boat.com";
            guest.Phone = "909090909";
            guest.State = "Texas";

            Result<Reservation> result = forreservation.AddGuestReservation(host, guest, StartDate, EndDate, 200M);

            Assert.IsFalse(result.success);
        }
        [Test]
        public void ShouldUpdateReservation()
        {
            Host host = new Host();
            host.Id = Guid.Parse("b6ddb844-b990-471a-8c0a-519d0777eb9b");
            host.Email = "hello@hello.com";

            Reservation reserv = new Reservation();
            reserv.Id = 2;
            reserv.StartDate = DateTime.Parse("06/10/2021");
            reserv.EndDate = DateTime.Parse("07/10/2021");
            reserv.GuestId = 1;
            reserv.Total = 400;
            Result<Reservation> result = forreservation.UpdateReservation(host, reserv, forreservation.GetReservationsByEmail(host.Email));

            Assert.IsTrue(result.success);
        }
        [Test]
        public void ShouldNotUpdateWhenEmpty()
        {
            Host host = new Host();

            host.LastName = "Hello";
            host.Email = "hello@hello.com";
            host.Phone = "(000)000000";
            host.Address = "0000 Nothing Dr";
            host.City = "Ocala";
            host.State = "TX";
            host.PostalCode = "10100";
            host.StandardRate = 100;
            host.WeekendRate = 1000;

            Reservation reserv = new Reservation();
            reserv.Id = 2;
            reserv.StartDate = DateTime.Parse("06/10/2021");
            reserv.EndDate = DateTime.Parse("07/10/2021");
            reserv.GuestId = 1;
            reserv.Total = 400;
            Result<Reservation> result = forreservation.UpdateReservation(host, reserv, forreservation.GetReservationsByEmail(host.Email));

            Assert.IsFalse(result.success);

        }
        [Test]
        public void ShouldFindByEmail()
        {
            System.Collections.Generic.List<Reservation> result = forreservation.GetReservationsByEmail("hello@hello.com");
            
            Assert.IsNotEmpty(result);
        }
      
       
        [Test]
        public void ShouldDeleteReservation()
        {
            Host host = new Host();
            host.Id = Guid.Parse("b6ddb844-b990-471a-8c0a-519d0777eb9b");
            host.Email = "hello@hello.com";

            Result<Reservation> result = forreservation.DeleteReservation(2, host);

            Assert.IsTrue(result.success);
        }
        [Test]
        public void ShouldFindTotalCost()
        {
            
            Host host = new Host();
            host.Id = Guid.Parse("b6ddb844-b990-471a-8c0a-519d0777eb9b");
            host.LastName = "Hello";
            host.Email = "hello@hello.com";
            host.Phone = "(000)000000";
            host.Address = "0000 Nothing Dr";
            host.City = "Ocala";
            host.State = "TX";
            host.PostalCode = "10100";
            host.StandardRate = 100;
            host.WeekendRate = 200;

            DateTime StartDate = DateTime.Parse("06/10/2021");
            DateTime EndDate = DateTime.Parse("06/15/2021");
            decimal total = forreservation.FindTotalCost(host, StartDate, EndDate);
            Assert.AreEqual(700M, total);
        }
       


    }
}