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
        private ILogger fileLogger;

        public ReservationFileRepository(string filePath, ILogger fileLogger)
        {
            this.filePath = filePath;
            this.fileLogger = fileLogger;

        }

        public List<Reservation> ViewReservationsByHost(Host host)
        {
            //file error below in var path
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
                fileLogger.Log(ex.ToString());
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
        public Result<Reservation> Delete(int Id, Host host)
        {
            if(host == null)
            {
                return null;
            }
            bool success = false;
            List<Reservation> all = ViewReservationsByHost(host);
            foreach (var r in from Reservation r in all
                              where r.Id == Id
                              select r)
            {
                success = all.Remove(r);
                break;
            }

            Result<Reservation> result = new Result<Reservation>();
            if (success == true)
            {
                Write(all, host);
                return result;
            }
            else
            {
                result.AddMessage("Error: Reservation Not found!");
                return result;
            }

            

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
                fileLogger.Log(ex.ToString());
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
       

        public Result<Reservation> Update(Host host, Reservation reservation)
        {
            if (host == null)
            {
                return null;
            }
            bool success = false;
            List<Reservation> all = ViewReservationsByHost(host);
            foreach (var r in from Reservation r in all
                              where r.Id == reservation.Id
                              select r)
            {
                r.StartDate = reservation.StartDate;
                r.EndDate = reservation.EndDate;
                r.Total = reservation.Total;
                success = true;
                break;
            }

            Result<Reservation> result = new Result<Reservation>();
            if (success == true)
            {
                Write(all, host);
                return result;
            }
            else
            {
                result.AddMessage("Error: Reservation Not found!");
                return result;
            }
        }       

     

        public List<Reservation> ViewByLocation(string state)
        {
            throw new NotImplementedException();
        }
    }
}
