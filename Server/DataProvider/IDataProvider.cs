using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.DataProvider
{
    public interface IDataProvider
    {
        object GetTrainingData();
        void SaveTrainingData(object modelInfos);
    }
}
