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

        private readonly List<Guest> guests = new List<Guest>();

        public GuestFileRepositoryDouble()
        {
            Guest guest = new Guest();
            guest.GuestId = 1;
            guest.FirstName = "ILean";
            guest.LastName = "Boat";
            guest.Email = "boat@boat.com";
            guest.Phone = "909090909";
            guest.State = "Texas";
            guests.Add(guest);
        }
        public Guest Add(Guest guest)
        {
            guests.Add(guest);
            return guest;
        }

        public List<Guest> FindAll()
        {
            return guests;
        }

        public Guest FindGuestById(int id)
        {
            return FindAll().FirstOrDefault(i => i.GuestId == id);
        }

        public Guest ViewByEmail(string email)
        {
            Guest guest =  FindAll().FirstOrDefault(i => i.Email.Equals(email));
            return guest;
            //throw new NotImplementedException();
        }

        
    }
}
