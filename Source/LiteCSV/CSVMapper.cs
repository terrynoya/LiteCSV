using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCSV
{
    public class CSVMapper
    {
        public static T MapObject<T>(CSVParser parser, List<string> lineData)
        {
            object searializedData = parser.GetData(lineData);
            T t = default(T);
            try
            {
                t = (T) Convert.ChangeType(searializedData, typeof (T));
            }
            catch (InvalidCastException)
            {

            }
            return t;
        }

        public static List<T> Map<T>(CSVParser parser, CSVFile file)
        {
            List<T> rlt = new List<T>();
            int count = file.Datas.Count;
            for (int i = 0; i < count; i++)
            {
                CSVLineData lineData = file.Datas[i];
                T t = MapObject<T>(parser, lineData.Datas);
                if (t != null)
                {
                    rlt.Add(t);
                }
            }
            return rlt;
        }

        public static List<T> Map<T>(CSVParser parser, string text, int dataStartLine,
            string lineToken = CSVToken.CSV_LINE_TOKEN, string columnToken = CSVToken.CSV_COLUMN_TOKEN)
        {
            List<T> rlt = new List<T>();

            string[] lines = text.Split(new string[] {lineToken}, StringSplitOptions.None);

            for (int i = dataStartLine; i < lines.Length - dataStartLine; i++)
            {
                List<string> datas = GetLineDatas(lines[i], columnToken);
                T t = MapObject<T>(parser, datas);
                if (t != null)
                {
                    rlt.Add(t);
                }
            }
            return rlt;
        }

        public static List<string> GetLineDatas(string line, string columnToken)
        {
            List<string> rlt = new List<string>();
            string[] datas = line.Split(new string[] {columnToken}, StringSplitOptions.None);
            for (int i = 0; i < datas.Length; i++)
            {
                string data = datas[i];
                rlt.Add(data);
            }
            return rlt;
        }
    }
}