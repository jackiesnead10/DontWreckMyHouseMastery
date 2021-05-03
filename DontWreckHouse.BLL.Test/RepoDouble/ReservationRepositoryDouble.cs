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
        private readonly List<Reservation> reservations = new List<Reservation>();
        DateTime startDate = new DateTime(2020, 6, 26);
        DateTime endDate = new DateTime(2020, 6, 29);

        public ReservationRepositoryDouble()
        {
            Reservation reserv = new Reservation();
            reserv.Id = 2;
            reserv.StartDate = startDate;
            reserv.EndDate = endDate;
            reserv.GuestId = 1;
            reserv.Total = 200;
            reservations.Add(reserv);
        }
      

        public Result<Reservation> Add(Reservation reserv, Host host)
        {
            List<Reservation> reserve = ViewReservationsByHost(host);
            reserv.Id = reserve.Max(i => i.Id) + 1;
            reserve.Add(reserv);
            Result<Reservation> result = new Result<Reservation>();
            return result;
        }
     

        public Result<Reservation> Delete(int Id, Host host)
        {
           foreach(Reservation r in reservations)
            {
                if (r.Id == Id)
                {
                    reservations.Remove(r);
                    return new Result<Reservation>();
                }
            }

            Result<Reservation> result = new Result<Reservation>();
            result.AddMessage("Could not find reservation!");
            return result;
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

        public Result<Reservation> Update(Host host, Reservation reservation)
        {
            Result<Reservation> reservationResult = new Result<Reservation>();

            if (!host.Id.Equals(Guid.Parse("b6ddb844-b990-471a-8c0a-519d0777eb9b")))
            {
                reservationResult.AddMessage("Host is not in repository!");
                
            }
                return reservationResult;
            
          
        }

        public List<Reservation> ViewByLocation(string state)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> ViewReservationsByHost(Host host)
        {
            return reservations;
           // return new List<Reservation>(reserervations);

        }

    
    }
}
