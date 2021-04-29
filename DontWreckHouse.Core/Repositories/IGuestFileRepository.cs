using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.Core
{
    public interface IGuestFileRepository
    {
        Guest ViewByEmail(string email);
        
            Guest Add(Guest guest);

        List<Guest> FindAll();

        Guest FindGuestById(int id);
        
		
    }
}
