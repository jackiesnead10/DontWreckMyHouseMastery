using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.BLL
{
    public class Result<T>
    {
        private List<string> messages = new List<string>();
        public List<string> Messages => new List<string>(messages);
        public bool success => messages.Count == 0;
        public Decimal addDecimalMess = new decimal();

        public void AddMessage(string message)
        {
            messages.Add(message);
        }
       
    }
}
