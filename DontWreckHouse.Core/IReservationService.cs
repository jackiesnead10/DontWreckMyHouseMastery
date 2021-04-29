using DontWreckHouse.BLL;
using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.Core
{
    public interface IReservationService
    {
        Result<Reservation> Add(Reservation reservation);
        //	Result<Reservation> ViewByEmail(string email);
        Result<Reservation> ViewByLocation(string location);
        Result<Reservation> Update(int id, Reservation reservation);
		Result<Reservation> Delete (int id);
    }
}
