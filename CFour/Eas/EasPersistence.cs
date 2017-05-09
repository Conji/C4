using CFour.Api;
using Sec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Eas
{
    public class EasPersistence : IDataPersistence
    {
        private Dictionary<string, EasReport> m_Reports;
        private SecFile m_PermissionsFile;
        private string m_PermFileName = "permissions";
        private Dictionary<int, AdminGroup> m_Groups;

        public EasPersistence()
        {
            m_Reports = new Dictionary<string, EasReport>();
            m_Groups = new Dictionary<int, AdminGroup>();
        }

        public IDataReport GetReportFor(string id)
        {
            if (!m_Reports.ContainsKey(id))
                return null;
            return m_Reports[id];
        }

        public void Load()
        {
            if (File.Exists(m_PermFileName))
            {
                m_PermissionsFile = SecFile.Open(m_PermFileName);
                foreach (var table in m_PermissionsFile)
                {
                    var roleId = int.Parse(table.Name);
                    var permissions = table.KeylessData.ToArray();
                    var roleName = (string)table.Get<SecString>("name");
                }
            }
            
            if (!Directory.Exists("eas"))
            {
                Directory.CreateDirectory("eas");
                return;
            }
            foreach (var file in Directory.EnumerateFiles("eas", "*.easj"))
            {
                var eas = EasReport.FromJson(File.ReadAllText(file));
                m_Reports.Add(eas.PlayerXuid, eas);
            }
        }

        public void Save(string id)
        {
            if (!m_Reports.TryGetValue(id, out var eas)) return;
            var file = $"eas\\{id}.easj";
            File.WriteAllText(file, eas.ToJson());
        }

        public void Save()
        {
            foreach (var entry in m_Reports)
            {
                Save(entry.Key);
            }
        }

        public AdminGroup GetGroup(int adminLevel)
        {
            if (m_Groups.ContainsKey(adminLevel))
                return m_Groups[adminLevel];
            return null;
        }
    }
}
