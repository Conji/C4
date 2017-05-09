using CFour.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Xlr
{
    public class XlrManager : IDataManager 
    {
        public IDataPersistence Persistence { get; }

        public XlrManager(CFour c4)
        {
            Persistence = new XlrPersistence();
            
        }

        public void Initialize()
        {

        }

        public XlrReport GetXlrFor(string id)
        {
            return ((XlrPersistence)Persistence).GetReportFor(id) as XlrReport;
        }

        public void SetXlrFor(string id, XlrReport report)
        {
            ((XlrPersistence)Persistence).UpdateReportFor(id, report);
        }
    }
}
