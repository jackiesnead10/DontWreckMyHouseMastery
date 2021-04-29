using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.Core
{
    public interface IHostFileRepository
    {
        Host ViewByEmail(string email);
        List<Host> FindAll();
        List<Host> ViewByLocation(string location);
    }
}
