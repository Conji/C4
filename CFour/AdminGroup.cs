using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    public class AdminGroup
    {
        public string Name { get; }
        public int Level { get; }
        public string[] Permissions { get; }

        public AdminGroup(string name, int level, string[] perms)
        {
            Name = name;
            Level = level;
            Permissions = perms;
        }
    }
}
