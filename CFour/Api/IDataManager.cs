using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour.Api
{
    public interface IDataManager
    {
        IDataPersistence Persistence { get; }
        void Initialize();
    }
}
