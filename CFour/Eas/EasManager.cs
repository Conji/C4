using CFour.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Eas
{
    public class EasManager : IDataManager
    {
        public IDataPersistence Persistence { get; }

        public EasManager(CFour c4)
        {
            Persistence = new EasPersistence();
        }

        public void Initialize()
        {

        }

        public EasReport GetEasFor(string id)
        {
            return Persistence.GetReportFor(id) as EasReport;
        }

        public bool IsAdmin(string id)
        {
            var report = Persistence.GetReportFor(id) as EasReport;
            if (report == null) return false;
            return report.AdminLevel > 0;
        }
    }
}
