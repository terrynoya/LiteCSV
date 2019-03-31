<a href="https://996.icu"><img src="https://img.shields.io/badge/link-996.icu-red.svg"></a>

# LiteCSV
a lightweight csv table parser in C# mainly for unity but also use in .net

# [中文版如何使用](/Docs/README_CN.md)

=====
# How To Use

# Read a file
suppose you have an addressbook.csv to read,the content is like below

Id,Name,Desc,Data0,Data1,Data2

1,User0,Todo,0,0.1,TRUE

2,User1,Todo,1,0.2,FALSE

3,User2,Todo,2,0.3,TRUE

# Method One. CSVFile->CSVMapper

## 1.use CSVfile to read csv

```csharp
TextAsset txtAssest = Resources.Load<TextAsset>(path);
string content = txtAssest.text;
int dataStartLineNumber = 1;
CSVFile file = new CSVFile(dataStartLineNumber);
file.Parse(content);
```

CSVFile constructor need three params


**dataStartLineNumber**：data start line，in this case is from line 2，so dataStartLineNumber=1。

**lineToken**：line seperator，default value is "\r\n"。

**columnToken**：column seperator,default value is ","。


so it is possible to parse a none csv style data file
such like line seperator is "\r"，and column seperator is "\t"。

CSVFile will decode the file data as CSVLine structure，a CSVLine is represent a single line data

## 2.Searialize

### a.create a cs file AddressBookData.cs to represent searialized data

```csharp
public class AddressBookData
{
    public string Id;
    public string Name;
    public string Desc;
    public string Data0;
    public string Data1;
    public string Data2;
}
```

### b.create a CSVParser sub class to read CSLineData

```csharp
public class AddressBookDataParser:CSVParser
{
    //read data here
    public override object GetData(List<string> datas)
    {
        AddressBookData rlt = new AddressBookData();
        rlt.Id = datas[0];
        rlt.Name = datas[1];
        rlt.Desc = datas[2];
        rlt.Data0 = datas[3];
        rlt.Data1 = datas[4];
        rlt.Data2 = datas[5];
        return rlt;
    }
}
```

### c.use CSVMapper.Map to searialize

```csharp
List<AddressBookData> list = CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), file);
```

# Method Two, use CSVMapper directly

if you dont need to have the csvfile structure, you can use CSVMapper to searilize data directly

```csharp
CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), content,1);
```

# Difference between method one and two

if you want to keep the cs data structure in the csvfile for later use such as save the file to csv later,you need to use method one

if not, use method two

because method two iterate the data file only once

method one iterate twice,one for csvfile, one for csvmap

# unserialize ，modify and save CSVFile to CSV text

## create a CSVDataWriter sub class，use it to write AdressBookData back to CSVLineData

```csharp

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

```

## save a single AdressBookData

modify data property Name

```csharp

List<AddressBookData> list = CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), file);
AddressBookData data = list[0];
data.Name = "modify";
AdressBookDataWriter writer = new AdressBookDataWriter();
CSVLineData lineToModify = file.GetLineDataAt(0);
writer.SetData(data, lineToModify);


string finalCSVText = file.ToCSV();

```

you need to keep the relationship between CSVFile and List<AddressBookData>

## save all AdressBookData List

if the data number between list and csfile is not coincident,use CSVMapper.ToCSV method

```csharp

CSVMapper.ToCSV(new AdressBookDataWriter(),file,list);
finalCSVText = file.ToCSV();

```

CSVFile.ToCSV() can output a csv text

# Code Auto Generation

if your searilized data has thounds of property,it's tedious to write it by hand, you can try the CSVCSFileCreator to generate file for you


```csharp

CSVCSFileData classFileData = CSVCSFileCreator.CreateDataClassFile("AdBbookData", file.GetHeaderAt(0));
//here is the file content
Debug.Log(classFileData.SourceCode);

```

a Parser file can alse be auto generated

```csharp
CSVCSFileData parserClassFileData = CSVCSFileCreator.CreateParserClassFile("AdBbookData", file);
Debug.Log(parserClassFileData.SourceCode);
```

you can reference the CSVCSFileCreator source code,and write your own code autogen tool.

