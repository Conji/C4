using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Events
{
    public class PlayerKickedEventArgs : EventArgs
    {
        public int AdminId { get; }
        public int OffenderId { get; }
        public string Reason { get; }
    }
}
