using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using DontWreckHouse.Core.exceptions;

using System.IO;


using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckHouse.Core;

namespace DontWreckHouse.DAL
{
    public class HostFileRepository : IHostFileRepository
    {
        private const string HEADER = "id, first name, last name, state";
        private readonly string filePath;

        public HostFileRepository(string filePath)
        {
            this.filePath = filePath;
        }


        public List<Host> FindAll()
        {
            var Host = new List<Host>();
            if (!File.Exists(filePath))
            {
                return Host;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not read Host Email", ex);
            }
            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Host host = Deserialize(fields);
                if (host != null)
                {
                    Host.Add(host);
                }
            }
            return Host;
        }


        private Host Deserialize(string[] fields)
        {
            if (fields.Length != 10)
            {
                return null;
            }

            Host result = new Host();
            result.Id = Guid.Parse(fields[0]);
            result.LastName = fields[1];
            result.Email = fields[2];
            result.Phone = fields[3];
            result.Address = fields[4];
            result.City = fields[5];
            result.State = fields[6];
            result.PostalCode = fields[7];
            result.StandardRate = decimal.Parse(fields[8]);
            result.WeekendRate = decimal.Parse(fields[9]);
           


            return result;
        }
        public Host ViewByEmail(string email)
        {
           
            return FindAll().FirstOrDefault(i => i.Email.Equals(email));
        }
        
        public List<Host> ViewByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
