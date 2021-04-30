using DontWreckHouse.Core;
using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.BLL.Test
{
    public class GuestFileRepositoryDouble : IGuestFileRepository
    {

        private readonly List<Guest> guest = new List<Guest>();

        public GuestFileRepositoryDouble()
        {
            Guest guest = new Guest();
            guest.GuestId = 1;
            guest.FirstName = "ILean";
            guest.LastName = "Boat";
            guest.Email = "boat@boat.com";
            guest.Phone = "909090909";
            guest.State = "Texas";
        }
        public Guest Add(Guest guest)
        {
            
            return guest;
        }

        public List<Guest> FindAll()
        {
            return new List<Guest>(guest);
        }

        public Guest FindGuestById(int id)
        {
            return FindAll().FirstOrDefault(i => i.GuestId == id);
        }

        public List<Guest> ViewByEmail(string email)
        {
            throw new NotImplementedException();
        }

        Guest IGuestFileRepository.ViewByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
