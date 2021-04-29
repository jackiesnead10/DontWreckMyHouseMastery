using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    
    
        public enum MainMenuOption
        {
            Exit,
            ViewReservationsForHost,
            AddReservation,
            UpdateReservation,
            DeleteReservation,
            Nothing
            
            
        }

        public static class MainMenuOptionExtensions
        {
            public static string ToLabel(this MainMenuOption option) => option switch
            {
                MainMenuOption.Exit => "Exit",
                MainMenuOption.ViewReservationsForHost => "View Reservations for Host",
                MainMenuOption.AddReservation => "Make a reservation",
                MainMenuOption.UpdateReservation => "Edit a Reservation",
                MainMenuOption.DeleteReservation => "Cancel a reservation",
                

                _ => throw new NotImplementedException()
            };
        public static bool IsHidden(this MainMenuOption option) => option switch
        {
            MainMenuOption.Nothing => true,
            _ => false
        };

    }
    
}
