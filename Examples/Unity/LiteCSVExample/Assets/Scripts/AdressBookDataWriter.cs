using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteCSV;

public class AdressBookDataWriter:CSVDataWriter
{
    public override void SetData(object data, CSVLineData lineData)
    {
        AddressBookData abData = data as AddressBookData;
        if (abData == null)
        {
            return;
        }
        lineData.Clear();
        lineData.AddData(abData.Id);
        lineData.AddData(abData.Name);
        lineData.AddData(abData.Desc);
        lineData.AddData(abData.Data0);
        lineData.AddData(abData.Data1);
        lineData.AddData(abData.Data2);
    }
}
