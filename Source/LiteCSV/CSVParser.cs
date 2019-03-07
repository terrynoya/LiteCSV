using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCSV
{
    /// <summary>
    /// convert line data to user custom class
    /// </summary>
    public abstract class CSVParser
    {
        public abstract object GetData(List<string> datas);
    }
}