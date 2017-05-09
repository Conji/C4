using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    public class CommandArgument : DynamicObject 
    {
        private readonly Dictionary<string, object> m_Properties;

        public CommandArgument(Dictionary<string, object> properties)
        {
            m_Properties = properties;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return m_Properties.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return m_Properties.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (m_Properties.ContainsKey(binder.Name))
            {
                m_Properties[binder.Name] = value;
                return true;
            }
            return false;
        }
    }
}
