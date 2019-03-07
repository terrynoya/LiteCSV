using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCSV
{
    public class CSVCSFileCreator
    {

        public static CSVCSFileData CreateDataClassFile(string className, CSVLineData header)
        {
            CSVCSFileData fileData = new CSVCSFileData();
            fileData.ClassName = className;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public class " + className);
            sb.AppendLine("{");
            for (int i = 0; i < header.Datas.Count; i++)
            {
                string propName = header.Datas[i];
                sb.AppendLine("\tpublic string " + propName + ";");
            }
            sb.AppendLine("}");
            fileData.SourceCode = sb.ToString();
            return fileData;
        }

        public static CSVCSFileData CreateParserClassFile(string className, CSVFile file)
        {
            CSVCSFileData fileData = new CSVCSFileData();

            CSVLineData header = file.GetHeaderAt(0);
            //parser name
            string parserClassName = className + "Parser";

            fileData.ClassName = parserClassName;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("public class " + parserClassName + ":CSVParser");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic override object GetData(List<string> datas)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\t" + className + " data = new " + className + "();");
            int propLen = header.Datas.Count;
            for (int i = 0; i < propLen; i++)
            {
                string propName = header.Datas[i];
                sb.AppendLine("\t\t" + "data." + propName + " = datas[" + i + "];");
            }
            sb.AppendLine("\t\treturn data;");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            fileData.SourceCode = sb.ToString();

            return fileData;
        }
    }
}