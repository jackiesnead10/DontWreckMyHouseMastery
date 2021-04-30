using DontWreckHouse.Core;
using DontWreckHouse.Core.Models;
using DontWreckHouse.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.BLL.Test
{
    class ReservationRepositoryDouble : IReservationsFileRepository
    {
        private readonly List<Reservation> reserervations = new List<Reservation>();
        DateTime startDate = new DateTime(2020, 6, 26);
        DateTime endDate = new DateTime(2020, 6, 29);

        public ReservationRepositoryDouble()
        {
            Reservation reserv = new Reservation();
            reserv.Id = 2;
            reserv.StartDate = startDate;
            reserv.EndDate = endDate;
            reserv.GuestId = 3;
            reserv.Total = 200;
        }
        public Reservation Add(Reservation reservation)
        {
            throw new NotImplementedException();

        }

        public Result<Reservation> Add(Reservation reservation, Host host)
        {
            throw new NotImplementedException();
        }

        public Result<Reservation> Delete(int id)
        {
            throw new NotImplementedException();
        }
        /*
        public Host FindByEmail(string email)
        {
            return HostFileRepository.ViewByEmail(email);

        }

        public List<Reservation> GetReservationsByEmail(string email)
        {
            Host host = hostFileRepository.ViewByEmail(email);
            List<Reservation> reservations = reservationFileRepository.ViewReservationsByHost(host);
            return reservations;


        }
        */
        public bool Update(int id, Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> ViewByLocation(string state)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> ViewReservationsByHost(Host host)
        {
            throw new NotImplementedException();
        }
    }
}
