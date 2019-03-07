using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCSV
{
    public class CSVLineData
    {
        private List<string> _datas;

        public CSVLineData(List<string> datas)
        {
            this._datas = datas;
        }

        public CSVLineData()
        {
            this._datas = new List<string>();
        }

        public int Count
        {
            get { return this._datas.Count; }
        }

        public List<string> Datas
        {
            get { return this._datas; }
        }

        public void Clear()
        {
            this._datas.Clear();
        }

        public void AddData(string value)
        {
            this._datas.Add(value);
        }

        public void SetData(int column, string value)
        {
            if (column < 0 || column > this._datas.Count - 1)
            {
                return;
            }
            this._datas[column] = value;
        }
    }
}