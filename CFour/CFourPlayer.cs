using CFour.Eas;
using CFour.Xlr;
using InfinityScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    public class CFourPlayer
    {
        public Entity BaseEntity { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAuthenticated { get; set; } // only used if admin
        public int PasswordAttempts { get; set; }
        public XlrReport Xlr { get; set; }
        public EasReport Eas { get; set; }

        public string Hwid => BaseEntity.GetHwid();
        public string Guid => BaseEntity.GetGUID();
        public string Xuid => BaseEntity.GetXUID();
        public string IPAddress => BaseEntity.IP.ToString();

        public CFourPlayer(Entity baseEntity, XlrReport xlr, EasReport eas)
        {
            BaseEntity = baseEntity;
            if (eas.AdminLevel > 0)
            {
                IsAdmin = true;
                IsAuthenticated = false;
            }
            else
            {
                IsAdmin = false;
                IsAuthenticated = true;
            }
            PasswordAttempts = 0;
        }
    }
}
