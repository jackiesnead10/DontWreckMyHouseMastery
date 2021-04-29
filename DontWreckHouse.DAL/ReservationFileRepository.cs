using DontWreckHouse.BLL;
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
    public class ReservationFileRepository : IReservationsFileRepository
    {
        private readonly string filePath;
        private const string HEADER = "Id,StartDate,EndDate,GuestId,Total";
        public ReservationFileRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Reservation> ViewReservationsByHost(Host host)
        {
            var Reservations = new List<Reservation>();
            var path = Path.Combine(filePath, host.Id + ".csv");
            if (!File.Exists(path))
            {
                return Reservations;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not read items", ex);
            }

            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Reservation reservation = Deserialize(fields);
                if (reservation != null)
                {
                    Reservations.Add(reservation);
                }
            }
            return Reservations;
        }

        private Reservation Deserialize(string[] fields)
        {
            if (fields.Length != 5)
            {
                return null;
            }

            Reservation result = new Reservation();
            result.Id = int.Parse(fields[0]);
            result.StartDate = DateTime.Parse(fields[1]);
            result.EndDate = DateTime.Parse(fields[2]);
            result.GuestId = int.Parse(fields[3]);
            result.Total = Decimal.Parse(fields[4]);

            return result;
        }
      
        
            public Result<Reservation> Add(Reservation reservation, Host host)
            {
                if (reservation == null)
                {
                    return null;
                }

                List<Reservation> all = ViewReservationsByHost(host);

                int nextId = (all.Count == 0 ? 0 : all.Max(i => i.Id)) + 1;

                reservation.Id = nextId;

                all.Add(reservation);
                Write(all, host);
            Result<Reservation> result = new Result<Reservation>();
           
                return result;
            }

        private void Write(List<Reservation> reservations, Host host)
        {
            var path = Path.Combine(filePath, host.Id + ".csv");
            try
            {
                using StreamWriter writer = new StreamWriter(path);
                writer.WriteLine(HEADER);

                if (reservations == null)
                {
                    return;
                }

                foreach (var reservation in reservations)
                {
                    writer.WriteLine(Serialize(reservation));
                }
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not save reservations", ex);
            }
        }

        private string Serialize(Reservation reservation)
        {
            return string.Format("{0},{1},{2},{3},{4:0.00}",
                    reservation.Id,
                    reservation.StartDate,
                    reservation.EndDate,
                    reservation.GuestId,
                    reservation.Total);
                   
        }
        public Result<Reservation> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Reservation reservation)
        {
            throw new NotImplementedException();
        }

     

        public List<Reservation> ViewByLocation(string state)
        {
            throw new NotImplementedException();
        }
    }
}
