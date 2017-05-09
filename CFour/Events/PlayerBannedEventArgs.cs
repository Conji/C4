using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Events
{
    public class PlayerBannedEventArgs : EventArgs
    {
        public int AdminId { get; }
        public int OffenderId { get; }
        public string Reason { get; }
        public TimeSpan Duration { get; }
        public DateTime OccuredOn { get; }
        public bool IsGlobal { get; }

        public PlayerBannedEventArgs(int admin, int offender, string reason, TimeSpan duration, DateTime occured, bool global)
        {
            AdminId = admin;
            OffenderId = offender;
            Reason = reason;
            Duration = duration;
            OccuredOn = occured;
            IsGlobal = global;
        }
    }
}
