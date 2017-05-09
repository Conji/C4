using CFour.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CFour.Xlr
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class XlrReport : IDataReport
    {
        [JsonProperty("playerId")]
        public string PlayerId { get; set; }
        [JsonProperty("kills")]
        public int Kills { get; set; }
        [JsonProperty("deaths")]
        public int Deaths { get; set; }
        [JsonProperty("headshots")]
        public int Headshots { get; set; }
        [JsonProperty("moabs")]
        public int Moabs { get; set; }
        [JsonProperty("assists")]
        public int Assists { get; set; }
        [JsonProperty("suicides")]
        public int Suicides { get; set; }
        [JsonProperty("killsByWeapons")]
        public Dictionary<string, int> KillsByWeapon { get; set; }
        [JsonProperty("lastLogin")]
        public DateTime LastLogin { get; set; }
        [JsonProperty("timesPlayed")]
        public DateTime TimesPlayed { get; set; }
        [JsonProperty("longestKillstreak")]
        public int LongestKillstreak { get; set; }
        [JsonProperty("longestDeathstreak")]
        public int LongestDeathstreak { get; set; }

        public static XlrReport FromJson(string json)
        {
            return JsonConvert.DeserializeObject<XlrReport>(json);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
