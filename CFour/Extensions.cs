using InfinityScript;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    public static class Extensions
    {
        private static Dictionary<int, (int, int)> m_HwidLocs; 

        static Extensions()
        {
            m_HwidLocs = new Dictionary<int, (int, int)>();
            int num2 = 77792053;
            int num3 = 77792013;
            int num4 = 493192;
            for (var i = 0; i < 18; i++)
            {
                m_HwidLocs.Add(i, (num3 + i, num2 + i * num4));
            }
        }
        public static string[] Slice(this string input, int charLength)
        {
            var sectionCount = (int)Math.Ceiling(d: input.Length / charLength);
            var data = new string[sectionCount];
            for (var i = 0; i < sectionCount; i++)
            {
                data[i] = input.Substring(i * charLength, charLength);
            }
            return data;
        }

        public static dynamic ParseToCommand(this string input, string map)
        {
            var keys = map.Split(',').Select(k => k = k.Trim()).ToArray();
            var values = input.Split(',').Select(k => k = k.Trim()).ToArray();
            var data = new Dictionary<string, object>();
            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                object value;
                if (values.Length <= i)
                    value = null;
                else
                    value = values[i];
                data.Add(key, value);
            }
            return new CommandArgument(data);
        }

        public static string GetHwid(this Entity entity)
        {
            var entityId = entity.GetEntityNumber();
            if (entityId > 17) return null;
            var item = m_HwidLocs[entityId].Item2;
            var data = MemoryReader.Instance.ReadBytes(item, 12);
            return Encoding.UTF8.GetString(data);
        }
    }
}
