using DontWreckHouse.Core;
using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DontWreckHouse.BLL.Test
{
    class HostFileRepositoryDouble : IHostFileRepository
    {
        private readonly List<Host> hosts = new List<Host>();

        public HostFileRepositoryDouble()
        {
            Host host = new Host();
            host.Id = Guid.Parse("b6ddb844-b990-471a-8c0a-519d0777eb9b");
            host.LastName = "Hello";
            host.Email = "hello@hello.com";
            host.Phone = "(000)000000";
            host.Address = "0000 Nothing Dr";
            host.City = "Ocala";
            host.State = "TX";
            host.PostalCode = "10100";
            host.StandardRate = 100;
            host.WeekendRate = 1000;
            hosts.Add(host);

        }
        public List<Host> FindAll()
        {
            return hosts;
        }

        public Host ViewByEmail(string email)
        {
            return FindAll().FirstOrDefault(i => i.Email == email);
        }

      
    }
}
