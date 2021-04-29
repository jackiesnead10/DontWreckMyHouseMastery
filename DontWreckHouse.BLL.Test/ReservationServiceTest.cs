using NUnit.Framework;
using System;
using DontWreckHouse.Core.Models;
using DontWreckHouse.Core;
using DontWreckHouse.BLL;


namespace DontWreckHouse.BLL.Test
{
    public class Tests
    {
       
        
         ReservationService forreservation = new ReservationService(new ReservationRepositoryDouble(), new GuestFileRepositoryDouble(), new HostFileRepositoryDouble());
        

        [Test]
        public void ShouldAddReservation()
        {
            Assert.Pass();
        }
    }
}