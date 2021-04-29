using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.Core.Models
{
    public class Reservation
    {
         public int Id {get; set;} 
		 public DateTime StartDate {get; set;} 
         public DateTime EndDate { get; set; } 
         public int GuestId { get; set; } 
         public Decimal Total { get; set; } 
    }
}
