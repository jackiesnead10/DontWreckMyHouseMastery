using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckHouse.BLL;

namespace DontWreckHouse.Core
{
    public interface IReservationsFileRepository
    {
        Result<Reservation> Add(Reservation reservation, Host host);
        List<Reservation> ViewReservationsByHost(Host host);
        List<Reservation> ViewByLocation(string state);
        bool Update(int id, Reservation reservation);
        Result<Reservation> Delete(int id);

    
    }
}
