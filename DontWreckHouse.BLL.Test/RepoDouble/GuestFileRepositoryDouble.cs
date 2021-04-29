using DontWreckHouse.Core;
using DontWreckHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.BLL.Test
{
    class GuestFileRepositoryDouble : IGuestFileRepository
    {
        public Guest Add(Guest guest)
        {
            throw new NotImplementedException();
        }

        public List<Guest> FindAll()
        {
            throw new NotImplementedException();
        }

        public Guest FindGuestById(int id)
        {
            throw new NotImplementedException();
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
