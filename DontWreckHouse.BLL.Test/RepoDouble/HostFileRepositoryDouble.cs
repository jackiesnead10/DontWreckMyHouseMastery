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
        public List<Host> FindAll()
        {
            throw new NotImplementedException();
        }

        public Host ViewByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<Host> ViewByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
