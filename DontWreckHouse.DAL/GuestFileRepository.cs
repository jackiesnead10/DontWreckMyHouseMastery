using DontWreckHouse.Core;
using DontWreckHouse.Core.exceptions;
using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.DAL
{
   public class GuestFileRepository : IGuestFileRepository
    {
       
        private readonly string filePath;

        public GuestFileRepository(string filePath)
        {
            this.filePath = filePath;
        }
        /*
        public Guest Add(Guest guest)
        {
            if (guest == null)
            {
                return null;
            }

            List<Guest> all = FindAll();

            int nextId = (all.Count == 0 ? 0 : all.Max(i => i.GuestId)) + 1;

            guest.GuestId = nextId;

            all.Add(guest);
            Write(all);

            return guest;
        }
        */
        public List<Guest> FindAll()
        {
            var items = new List<Guest>();
            if (!File.Exists(filePath))
            {
                return items;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not read items", ex);
            }

            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);
                if (guest != null)
                {
                    items.Add(guest);
                }
            }
            return items;
        }
        //Use of Linq query
        public Guest FindById(int id)
        {
            //List<Guest> items = FindAll();
            //IEnumerable<Guest> guest = from i in items where i.GuestId == id select i;
            //return guest.FirstOrDefault();
            return FindAll().FirstOrDefault(i => i.GuestId == id);
        }

        private string Serialize(Guest guest)
        {
            return string.Format("{0},{1},{2},{3},{4},{5}, {3:0.00}",
                    guest.GuestId,
                    guest.FirstName,
                    guest.LastName,
                    guest.Email,
                    guest.Phone,
                    guest.State);
        }

        private Guest Deserialize(string[] fields)
        {
            if (fields.Length != 6)
            {
                return null;
            }

            Guest result = new Guest();
            result.GuestId = int.Parse(fields[0]);
            result.FirstName = fields[1];
            result.LastName = fields[2];
            result.Email = fields[3];
            result.Phone = fields[4];
            result.Phone = fields[5];
            return result;
        }

        public Guest ViewByEmail(string email)
        {
            return FindAll().FirstOrDefault(i => i.Email.Equals(email));
        }

        public Guest Add(Guest guest)
        {
            throw new NotImplementedException();
        }
        //return to reservationservice
        public Guest FindGuestById(int id)
        {
            return FindAll().FirstOrDefault(i => i.GuestId.Equals(id));

        }
    }
}
