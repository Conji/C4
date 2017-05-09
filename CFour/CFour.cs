using CFour.Eas;
using CFour.Events;
using CFour.Xlr;
using InfinityScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    // The logic portion of C4.
    public partial class CFour : BaseScript
    {
        /// <summary>
        ///     The persistence manager for this script.
        /// </summary>
        public PersistenceManager Persistence { get; set; }

        /// <summary>
        ///     The XLRManager for the script.
        /// </summary>
        public XlrManager Xlr { get; set; }
        /// <summary>
        ///     The EASManager for the script.
        /// </summary>
        public EasManager Eas { get; set; }
        /// <summary>
        ///     The memory reader for the script.
        /// </summary>
        public MemoryReader MemoryReader { get; set; }

        #region Events

        public event EventHandler<PlayerKickedEventArgs> PlayerKicked;
        public event EventHandler<PlayerBannedEventArgs> PlayerBanned;

        #endregion

        #region .ctor

        public CFour()
        {
            Tick += Update;
            PlayerConnected += HandlePlayerJoined;
            PlayerConnecting += HandlePlayerConnecting;
            PlayerDisconnected += HandlePlayerDisconnecting;

            Xlr = new XlrManager(this);
            Eas = new EasManager(this);
            
            
        }

        #endregion

        #region Fields

        private Dictionary<string, (CommandCallback Callback, bool PermissionsNeeded)> m_RegisteredCmds = new Dictionary<string, (CommandCallback Callback, bool PermissionsNeeded)>();
        private bool m_WatchAimAlert = true;
        private List<CFourPlayer> m_CFourPlayers = new List<CFourPlayer>();

        #endregion

        public override void OnStartGameType()
        {
            base.OnStartGameType();
            // initialize all readers and such here
            Xlr.Initialize();
            Eas.Initialize();
        }

        /// <summary>
        ///     Prints a message to all players.
        /// </summary>
        /// <param name="message"></param>
        public void Say(string message)
        {
            message = "[PM]" + message;
            foreach (var m in message.Slice(MaxMessageCharLength))
            {
                Utilities.RawSayAll(m);
            }
        }

        /// <summary>
        ///     Prints a message to the specified player.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        public void Say(Entity entity, string message)
        {
            foreach (var m in message.Slice(MaxMessageCharLength))
            {
                Utilities.RawSayTo(entity, message);
            }
        }

        public CFourPlayer GetPlayerFromId(int id)
        {
            return m_CFourPlayers.SingleOrDefault(e => e.BaseEntity.UserID == id);
        }

        public CFourPlayer GetPlayerFromName(string name)
        {
            var match = m_CFourPlayers.SingleOrDefault(e => e.BaseEntity.Name.ToLower() == name.ToLower());
            if (match != null) return match;
            var matches = m_CFourPlayers.Where(p => p.BaseEntity.Name.ToLower().StartsWith(name.ToLower()));
            if (matches.Count() == 1) return matches.ElementAt(0);
            return null;
        }
        
        /// <summary>
        ///     Called on each .Tick
        /// </summary>
        public void Update()
        {

        }

        public void HandlePlayerJoined(Entity player)
        {
            var c4 = GetPlayerFromId(player.UserID);
            if (c4.Hwid.Length != 32)
            {
                player.Kick();
            }
        }

        public void HandlePlayerConnecting(Entity player)
        {
            lock (m_CFourPlayers)
            {
                m_CFourPlayers.Add(new CFourPlayer(player, Xlr.GetXlrFor(player.GetGUID()), Eas.GetEasFor(player.GetHwid())));
            }
        }

        public void HandlePlayerDisconnecting(Entity player)
        {
            lock (m_CFourPlayers)
            {
                m_CFourPlayers.RemoveAll(p => p.BaseEntity.UserID == player.UserID);
            }
        }

        public override void OnSay(Entity player, string name, string message)
        {
            if (TryGetCommand(player, ref message, out var print, out var callback))
            {
                callback?.Invoke(message, player);
                if (print)
                    base.OnSay(player, name, message);
            }
            else
            {
                base.OnSay(player, name, message);
            }
        }

        public override EventEat OnSay2(Entity player, string name, string message)
        {
            if (TryGetCommand(player, ref message, out var print, out var callback))
            {
                callback?.Invoke(message, player);
                if (print)
                    base.OnSay(player, name, message);
                return EventEat.EatNone;
            }
            else
            {
                return base.OnSay2(player, name, message);
            }
        }

        public override EventEat OnSay3(Entity player, ChatType type, string name, ref string message)
        {
            if (TryGetCommand(player, ref message, out var print, out var callback))
            {
                callback?.Invoke(message, player);
                if (print)
                    base.OnSay(player, name, message);
                return EventEat.EatNone;
            }
            else
            {
                return base.OnSay2(player, name, message);
            }
        }

        public override void OnPlayerKilled(Entity player, Entity inflictor, Entity attacker, int damage, string mod, string weapon, Vector3 dir, string hitLoc)
        {
            base.OnPlayerKilled(player, inflictor, attacker, damage, mod, weapon, dir, hitLoc);
        }

        #region Private Methods

        private void RegisterCommands()
        {
            RegisterCommand("report", Report, false);
            RegisterCommand("xlrtop", TopStats, false);
            RegisterCommand("xlr", PlayerStats, false);
            RegisterCommand("list", ListPlayers, true);
            RegisterCommand("login", Login, true);
            RegisterCommand("ban", BanPlayer, true);
            RegisterCommand("b", BanPlayer, true);
            RegisterCommand("kick", KickPlayer, true);
            RegisterCommand("k", KickPlayer, true);
            RegisterCommand("warn", WarnPlayer, true);
            RegisterCommand("w", WarnPlayer, true);
            RegisterCommand("unwarn", UnwarnPlayer, true);
            RegisterCommand("uw", UnwarnPlayer, true);
            RegisterCommand("uwu", CauseFuckYou, false);
            RegisterCommand("blowup", BlowupPlayer, true);
            RegisterCommand("setadmin", SetAdmin, true);
            RegisterCommand("scream", Scream, true);
            RegisterCommand("rules", Rules, false);
            RegisterCommand("votekick", VoteKick, false);
            RegisterCommand("vk", VoteKick, false);
            RegisterCommand("votemap", VoteMap, false);
            RegisterCommand("vm", VoteMap, false);
            RegisterCommand("help", Help, false);
            RegisterCommand("whisper", Whisper, false);
            RegisterCommand("pm", Whisper, false);

        }

        private void RegisterCommand(string command, CommandCallback callback, bool requiredPermissions)
        {
            m_RegisteredCmds.Add(command, (callback, requiredPermissions));
        }

        private bool TryGetCommand(Entity entity, ref string command, out bool printOutputForAll, out CommandCallback callback)
        {
            string cmd;
            var cmdStart = 0;
            // check if silent command
            if (command.StartsWith("/!"))
            {
                cmd = command.Split(' ')[0].ToLower().Substring(2);
                printOutputForAll = false;
                cmdStart = 2;
            }
            // check if loud command
            else if (command.StartsWith("!"))
            {
                cmd = command.Split(' ')[0].ToLower().Substring(1);
                printOutputForAll = true;
                cmdStart = 1;
            }
            // no command found
            else
            {
                printOutputForAll = true;
                callback = null;
                return false;
            }
            // adjust the command params to exclude the original command
            command = command.Substring(cmdStart + cmd.Length).Trim();
            var c4Player = GetPlayerFromId(entity.UserID);
            // check if the person is authenticated. If the admin isn't, no matter what, always tell they must login so
            // players can't spoof commands
            if (!c4Player.IsAuthenticated)
            {
                callback = AlertForLogin;
                return true;
            }
            if (!m_RegisteredCmds.ContainsKey(cmd))
            {
                callback = null;
                return false;
            }
            var cb = m_RegisteredCmds[cmd];
            // do a check if perms are needed for the command
            if (!cb.PermissionsNeeded)
            {
                callback = cb.Callback;
                return true;
            }
            // perms are needed from here on out.
            if (!c4Player.IsAdmin)
            {
                Say(c4Player.BaseEntity, "You don't have permission to use this command.");
                callback = null;
                return false;
            }
            // passed all checks. Proceed.
            callback = cb.Callback;
            return true;
        }

        private string AlertForLogin(string input, Entity caller)
        {
            Say(caller, "You must login first before using commands.");
            return "";
        }

        #endregion
    }
}
