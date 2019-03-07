using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCSV
{
    public class CSVFile
    {
        private int _dataStartLineNumber;

        private List<CSVLineData> _headers;
        private List<CSVLineData> _lineDatas;
        private string _lineToken;
        private string _columnToken;

        private int _columnCount = -1;

        public CSVFile(int dataStartLineNumber = 1, string lineToken = CSVToken.CSV_LINE_TOKEN,
            string columnToken = CSVToken.CSV_COLUMN_TOKEN)
        {
            this._dataStartLineNumber = dataStartLineNumber;
            this._lineToken = lineToken;
            this._columnToken = columnToken;
            this._headers = new List<CSVLineData>();
            this._lineDatas = new List<CSVLineData>();
        }

        public void Parse(string text)
        {
            string[] lines = text.Split(new string[] {this._lineToken}, StringSplitOptions.None);
            int headerCount = this._dataStartLineNumber;
            this.ParseHeaders(headerCount, lines);
            int dataOffset = this._dataStartLineNumber;
            this.ParseDatas(dataOffset, lines);
        }

        private void ParseDatas(int dataOffset, string[] lines)
        {
            for (int i = dataOffset; i < lines.Length - dataOffset; i++)
            {
                CSVLineData lineData = GetLineData(lines[i]);
                if (this.IsColumnCount(lineData))
                {
                    this._lineDatas.Add(lineData);
                }
            }
        }

        private void ParseHeaders(int count, string[] lines)
        {
            for (int i = 0; i < count; i++)
            {
                string line = lines[i];
                CSVLineData lineData = this.GetLineData(line);
                if (this.IsColumnCount(lineData))
                {
                    this._headers.Add(lineData);
                }
            }
        }

        private bool IsColumnCount(CSVLineData lineData)
        {
            int newCount = lineData.Count;
            if (this._columnCount == -1)
            {
                this._columnCount = newCount;
                return true;
            }
            else if(this._columnCount != newCount)
            {
                return false;
            }
            return true;
        }

        public CSVLineData GetHeaderAt(int index)
        {
            if (index < 0 || index > this._headers.Count - 1)
            {
                return null;
            }
            return this._headers[index];
        }

        public CSVLineData GetLineDataAt(int index)
        {
            if (index < 0 || index > this._lineDatas.Count - 1)
            {
                return null;
            }
            return this._lineDatas[index];
        }

        public List<CSVLineData> Datas
        {
            get { return this._lineDatas; }
        }

        public CSVLineData GetLineData(string line)
        {
            List<string> dataList = new List<string>();
            string[] datas = line.Split(new string[] {this._columnToken}, StringSplitOptions.None);
            for (int i = 0; i < datas.Length; i++)
            {
                string data = datas[i];
                dataList.Add(data);
            }
            CSVLineData rlt = new CSVLineData(dataList);
            return rlt;
        }

        public string ToCSV()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this._headers.Count; i++)
            {
                CSVLineData line = this._headers[i];
                string lineTxt = this.GetLineText(line);
                sb.Append(lineTxt);
                sb.Append(this._lineToken);
            }
            int dataLen = this._lineDatas.Count;
            for (int i = 0; i < dataLen; i++)
            {
                CSVLineData line = this._lineDatas[i];
                string lineTxt = this.GetLineText(line);
                sb.Append(lineTxt);
                if (i != dataLen - 1)
                {
                    sb.Append(this._lineToken);
                }
            }
            return sb.ToString();
        }

        public string GetLineText(CSVLineData lineData)
        {
            StringBuilder sb = new StringBuilder();
            List<string> dataList = lineData.Datas;
            int len = dataList.Count;
            for (int i = 0; i < len; i++)
            {
                string data = dataList[i];
                sb.Append(data);
                if (i != len - 1)
                {
                    sb.Append(this._columnToken);
                }
            }
            return sb.ToString();
        }
    }
}