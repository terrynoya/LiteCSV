using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteCSV;

public class AddressBookDataParser:CSVParser
{
    public override object GetData(List<string> datas)
    {
        AddressBookData rlt = new AddressBookData();
        rlt.Id = datas[0];
        rlt.Name = datas[1];
        return rlt;
    }
}
