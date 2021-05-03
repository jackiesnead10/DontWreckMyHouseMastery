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
        private readonly ILogger fileLogger;

        // public ReservationService(BLL.Test.ReservationServiceDouble reservationServiceDouble)
        //  {
        //   }

        public ReservationService(IReservationsFileRepository reservationFileRepository, IGuestFileRepository guestFileRepository, IHostFileRepository hostFileRepository, ILogger fileLogger )
        {
            this.hostFileRepository = hostFileRepository;
            this.guestFileRepository = guestFileRepository;
            this.reservationFileRepository = reservationFileRepository;
            this.fileLogger = fileLogger;
        }
        public Host FindByEmail(string email)
        {
            return hostFileRepository.ViewByEmail(email);

        }
        public Guest FindGuestByEmail(string email)
        {
            return guestFileRepository.ViewByEmail(email);

        }

        public List<Reservation> GetReservationsByEmail(string email)
        {
            Host host = hostFileRepository.ViewByEmail(email);
            List<Reservation> reservations = reservationFileRepository.ViewReservationsByHost(host);
            return reservations;


        }
        //Make a feature for adding guest start and end date and return a total
        public Result<Reservation> AddGuestReservation(Host host, Guest guest, DateTime startDate, DateTime endDate, decimal total)
        {
          
            List<Reservation> reservations = GetReservationsByEmail(host.Email);
         

            foreach (Reservation r in reservations)
            {
                if ((r.StartDate >= startDate) && (r.StartDate <= endDate))
                {
                    Result<Reservation> result = new Result<Reservation>();
                    result.AddMessage("Error: date conflicts with another reservation");
                    return result;
                }
                if ((r.EndDate >= startDate) && (r.EndDate <= endDate))
                {
                    Result<Reservation> result = new Result<Reservation>();
                    result.AddMessage("Error: date conflicts with another reservation");
                    return result;
                }
            }
           // reservation.GuestId = guestFileRepository.ViewByEmail(guestEmail).GuestId; exception thrown
            //
            Reservation reservation = new Reservation();
            reservation.EndDate = endDate;
            reservation.StartDate = startDate;
            
            reservation.GuestId = guest.GuestId;

            
            reservation.Total = total;
            Result<Reservation> resultval = Validate(reservation, host);
            return reservationFileRepository.Add(reservation, host);

        }
        //Validate and validate nulls
        //find weekend and weekday cost
        public decimal FindTotalCost(Host host, DateTime startdate, DateTime enddate)
        {
           

            decimal total = 0.00M;
            DateTime track = startdate;
            //  do
            // {
            do
            {
                if ((track.DayOfWeek == DayOfWeek.Friday) || (track.DayOfWeek == DayOfWeek.Saturday))
                {
                    total += host.WeekendRate;
                }
                else
                {
                    total += host.StandardRate;
                }
                track = track.AddDays(1);

            } while (track != enddate);



            return total;
                  //  Result<Reservation> result = new Result<Reservation>();
              //  result.AddMessage("Total: " + total);
            


           // } while ()
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
        public Guest FindGuestById(int guestId)
        {
            return guestFileRepository.FindGuestById(guestId);
       
       }
        public Result<Reservation> DeleteReservation(int Id, Host host)
        {
            return reservationFileRepository.Delete(Id ,host);

        }
        public Result<Reservation> UpdateReservation(Host host, Reservation reservation, List<Reservation> reservations)
        {
            foreach (Reservation r in reservations)
            {
                if ((r.StartDate >= reservation.StartDate) && (r.StartDate <= reservation.EndDate))
                {
                    Result<Reservation> result = new Result<Reservation>();
                    result.AddMessage("Error: date conflicts with another reservation");
                    return result;
                }
                if ((r.EndDate >= reservation.StartDate) && (r.EndDate <= reservation.EndDate))
                {
                    Result<Reservation> result = new Result<Reservation>();
                    result.AddMessage("Error: date conflicts with another reservation");
                    return result;
                }
            }

            return reservationFileRepository.Update(host, reservation);
        }
        public List<Reservation> HostHasGuestReservation(Guest guest, List<Reservation> reservations)
        {
            List<Reservation> guestreservations = new List<Reservation>();
            foreach (Reservation r in reservations)
            {
                if (r.GuestId == guest.GuestId)
                {
                    guestreservations.Add(r);
                }
                
            }
            return guestreservations;

        }

    }
}
