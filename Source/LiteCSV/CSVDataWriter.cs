using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCSV
{
    public abstract class CSVDataWriter
    {
        public abstract void SetData(object data, CSVLineData lineData);
    }
}