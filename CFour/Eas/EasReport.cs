using CFour.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Eas
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class EasReport : IDataReport
    {
        [JsonProperty("playerXuid")]
        public string PlayerXuid { get; set; }
        [JsonProperty("playerHwid")]
        public string PlayerHwid { get; set; }
        [JsonProperty("password")]
        public string PlayerPassword { get; set; } // this will be hashed so don't worry
        [JsonProperty("adminLvl")]
        public int AdminLevel { get; set; }

        public static EasReport FromJson(string input)
        {
            return JsonConvert.DeserializeObject<EasReport>(input);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
