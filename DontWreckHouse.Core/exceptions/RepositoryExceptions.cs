using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.Core.exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
