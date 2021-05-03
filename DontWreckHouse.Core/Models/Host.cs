using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.Core.Models
{
    public class Host
	{
		//changed guid Id into string Id
        public Guid Id {get; set;} 
		public string LastName {get; set;} 
		public string Email { get; set; } 
		public string Phone { get; set; }
		public string Address { get; set; } 
		public string City { get; set; } 
		public string State { get; set; } 
		public string PostalCode { get; set; } 
		public decimal StandardRate { get; set; } 
		public decimal WeekendRate { get; set; } 
    }
}
