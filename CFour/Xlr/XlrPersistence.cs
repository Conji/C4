using CFour.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Xlr
{
    public class XlrPersistence : IDataPersistence 
    {
        private Dictionary<string, XlrReport> m_Reports;

        public XlrPersistence()
        {
            m_Reports = new Dictionary<string, XlrReport>();
        }

        public void Load()
        {
            if (!Directory.Exists("xlr"))
            {
                Directory.CreateDirectory("xlr");
                return; // nothing to load
            }
            foreach (var file in Directory.EnumerateFiles("xlr", "*.xlrj"))
            {
                var data = File.ReadAllText(file);
                var xlr = XlrReport.FromJson(data);
                m_Reports.Add(xlr.PlayerId, xlr);
            }
        }

        public IDataReport GetReportFor(string playerId)
        {
            if (!m_Reports.TryGetValue(playerId, out var xlr))
                m_Reports.Add(playerId, new XlrReport());
            return m_Reports[playerId];
        }

        public void UpdateReportFor(string playerId, XlrReport report)
        {
            if (!m_Reports.ContainsKey(playerId))
                m_Reports.Add(playerId, report);
            else
                m_Reports[playerId] = report;
        }

        public void Save(string id)
        {
            if (!m_Reports.ContainsKey(id))
                m_Reports.Add(id, new XlrReport());
            var xlr = m_Reports[id];
            var file = $"xlr\\{id}.xlrj";
            File.WriteAllText(file, xlr.ToJson());
        }

        public void Save()
        {
            foreach (var entry in m_Reports)
            {
                Save(entry.Key);
            }
        }
    }
}
