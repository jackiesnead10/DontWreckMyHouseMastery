using System;
using System.Linq;
using System.Collections.Generic;
using DontWreckHouse.Core;
using DontWreckHouse.Core.Models;
using DontWreckHouse.DAL;

namespace DontWreckHouse.BLL
{
    public class ReservationService
    {
        private readonly IHostFileRepository hostFileRepository;
        private readonly IGuestFileRepository guestFileRepository;
        private readonly IReservationsFileRepository reservationFileRepository;

        // public ReservationService(BLL.Test.ReservationServiceDouble reservationServiceDouble)
        //  {
        //   }

        public ReservationService(IReservationsFileRepository reservationFileRepository, IGuestFileRepository guestFileRepository, IHostFileRepository hostFileRepository)
        {
            this.hostFileRepository = hostFileRepository;
            this.guestFileRepository = guestFileRepository;
            this.reservationFileRepository = reservationFileRepository;

        }
        public Host FindByEmail(string email)
        {
            return hostFileRepository.ViewByEmail(email);

        }

        public List<Reservation> GetReservationsByEmail(string email)
        {
            Host host = hostFileRepository.ViewByEmail(email);
            List<Reservation> reservations = reservationFileRepository.ViewReservationsByHost(host);
            return reservations;


        }
        //Make a feature for adding guest start and end date and return a total
        public Result<Reservation> AddGuestReservation(string hostEmail, string guestEmail, DateTime startDate, DateTime endDate)
        {
            List<Reservation> reservations = GetReservationsByEmail(hostEmail);

            foreach (Reservation r in reservations)
            {
                if ((r.StartDate > startDate) && (r.StartDate < endDate))
                {
                    Result<Reservation> result = new Result<Reservation>();
                    result.AddMessage("Error: date conflicts with another reservation");
                    return result;
                }
                if ((r.EndDate > startDate) && (r.EndDate < startDate))
                {
                    Result<Reservation> result = new Result<Reservation>();
                    result.AddMessage("Error: date conflicts with another reservation");
                    return result;
                }
            }
            Reservation reservation = new Reservation();
            reservation.EndDate = endDate;
            reservation.StartDate = startDate;
            reservation.GuestId = guestFileRepository.ViewByEmail(guestEmail).GuestId;

            Host host = hostFileRepository.ViewByEmail(hostEmail);
            reservation.Total = FindTotalCost(host, startDate, endDate);
            return reservationFileRepository.Add(reservation, host);

        }
        //Validate and validate nulls
        //find weekend and weekday cost
        private decimal FindTotalCost(Host host, DateTime startdate, DateTime enddate)
        {
            decimal total = 0.00M;
            DateTime track = startdate;
            while(track != enddate)
            {
                if((startdate.DayOfWeek == DayOfWeek.Friday) || (startdate.DayOfWeek == DayOfWeek.Saturday))
                {
                    total += host.WeekendRate;
                }
                else
                {
                    total += host.StandardRate;
                }
                track = track.AddDays(1);
            }
            return total;
        }
        private Result<Reservation> Validate(Reservation reservation, Host host)
        {
            Result<Reservation> result = ValidateNulls(reservation);
            if (!result.success)
            {
                return result;
            }

            ValidateFields(reservation, result);
            if (!result.success)
            {
                return result;
            }

            ValidateChildrenExist(reservation, result, host);

            return result;
        }

        private Result<Reservation> ValidateNulls(Reservation reservation)
        {
            var result = new Result<Reservation>();

            if (reservation == null)
            {
                result.AddMessage("Nothing to save.");
                return result;
            }

            if (reservation.StartDate == DateTime.MinValue)
            {
                result.AddMessage("Date is required.");
            }

            if (reservation.EndDate.Date == DateTime.MinValue)
            {
                result.AddMessage("Date is required.");
            }

            return result;
        }

        private void ValidateFields(Reservation reservation, Result<Reservation> result)
        {
            // No overlapping dates
            // Start date must come before the end date
            // start date must be in the future

            if (reservation.StartDate > DateTime.Now)
            {
                result.AddMessage("Start date cannot be in the past.");
            }

            if (reservation.EndDate < reservation.StartDate)
            {
                result.AddMessage("End date must come after start date.");
            }
            if (reservation.EndDate == reservation.StartDate)
            {
                result.AddMessage("End Date cannot be same day as start date");
            }

        }

        private void ValidateChildrenExist(Reservation reservation, Result<Reservation> result, Host host)
        {
            if (reservation.GuestId == 0
                    || guestFileRepository.FindGuestById(reservation.GuestId) == null)
            {
                result.AddMessage("Guest does not exist.");
            }

            if (hostFileRepository.ViewByEmail(host.Email) == null)
            {
                result.AddMessage("Host does not exist.");
            }





        }
    }
}
