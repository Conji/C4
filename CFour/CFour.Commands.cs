using CFour.Eas;
using InfinityScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    // The commands portion of C4.
    public partial class CFour : BaseScript 
    {
        /// <summary>
        ///     Represents a command callback function that accepts arguments.
        /// </summary>
        /// <param name="input">The arguments for the command.</param>
        /// <param name="caller">The player executing the command.</param>
        /// <returns></returns>
        public delegate string CommandCallback(string input, Entity caller);

        public const string PrintLineStruc = "Output";
        /// <summary>
        ///     Prints the line to the server output.
        /// </summary>
        /// <param name="input"></param>
        public string PrintLine(string input, Entity caller)
        {
            Log.Info(input);
            
            return "";
        }

        public const string ReportStruc = "Username, Comment";
        public string Report(string input, Entity caller)
        {
            return "Reported";
        }

        public string TopStats(string input, Entity caller)
        {
            return "";
        }
        
        public string PlayerStats(string input, Entity caller)
        {
            return "";
        }

        public const string BanStruc = "Username, Reason, Duration";
        public string BanPlayer(string input, Entity caller)
        {
            var eas = (EasReport)Eas.Persistence.GetReportFor(caller.GetHwid());
            if (eas == null)
                return "";
            if (eas.PlayerXuid != caller.GetXUID())
                return "";
            
            dynamic command = input.ParseToCommand(BanStruc);
            var isPermaban = command.Duration == null;
            return "";
        }

        public const string ListStruc = "List";
        public string ListPlayers(string input, Entity caller)
        {
            return "";
        }

        public const string KickStruc = "Username, Reason";
        public string KickPlayer(string input, Entity caller)
        {
            return "";
        }

        public const string UnbanStruc = "Username";
        public string UnbanPlayer(string input, Entity caller)
        {
            return "";
        }

        public const string PasswordStruc = "Password";
        public string Password(string input, Entity caller)
        {
            return "";
        }

        public const string LoginStruc = "Login";
        public string Login(string input, Entity caller)
        {
            return "";
        }

        public const string SetAdminStruc = "Username, Level";
        public string SetAdmin(string input, Entity caller)
        {
            return "";
        }

        public const string WarnStruc = "Username, Warning";
        public string WarnPlayer(string input, Entity caller)
        {
            return "";
        }

        public const string UnwarnStruc = "Username";
        public string UnwarnPlayer(string input, Entity caller)
        {
            return "";
        }

        public const string BlowupStruc = "Username";
        public string BlowupPlayer(string input, Entity caller)
        {
            return "";
        }

        public string CauseFuckYou(string input, Entity caller)
        {
            return "OwO What's this?";
        }

        public string Scream(string input, Entity caller)
        {
            return "";
        }

        public string Rules(string input, Entity caller)
        {
            return "";
        }

        public string VoteKick(string input, Entity caller)
        {
            return "";
        }

        public string VoteMap(string input, Entity caller)
        {
            return "";
        }

        public string Help(string input, Entity caller)
        {
            return "";
        }

        public string Whisper(string input, Entity caller)
        {
            return "";
        }

        public string CheckPlayer(string input, Entity caller)
        {
            return "";
        }
    }
}
