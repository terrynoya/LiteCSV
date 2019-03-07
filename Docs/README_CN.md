# LiteCSV
轻量级csv解析类库，可以使用在unity和.net中
=====
# 如何使用

假设addressbook.csv中需要处理以下内容

Id,Name,Desc,Data0,Data1,Data2

1,User0,Todo,0,0.1,TRUE

2,User1,Todo,1,0.2,FALSE

3,User2,Todo,2,0.3,TRUE

# 方法一 CSVFile->CSVMapper

## 1.使用CSVfile读取csv
```csharp
TextAsset txtAssest = Resources.Load<TextAsset>(path);
string content = txtAssest.text;
int dataStartLineNumber = 1;
CSVFile file = new CSVFile(dataStartLineNumber);
file.Parse(content);
```

CSVFile构造函数中接受三个参数：


**dataStartLineNumber**：数据从第几行开始，这个案例中数据从第二行开始，所以dataStartLineNumber=1。

**lineToken**：行分隔符，默认是"\r\n"。

**columnToken**：列分隔符，默认是","。


因此可以支持解析非csv风格的文件
比如行分隔符是"\r"，列分隔符是"\t"。

CSVFile会将行数据解析为CSVLine，所以CSVLine对应着相应的行数据

## 2.序列化

### a.新建一个AddressBookData.cs用来保存序列化的数据
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
### b.新建并实现一个CSVParser的子类，读取CSLineData
```csharp
public class AddressBookDataParser:CSVParser
{
    //在这里实现解析逻辑
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

### c.使用CSVMapper.Map进行序列化

```csharp
List<AddressBookData> list = CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), file);
```

# 方法二 直接使用CSVMapper

如果你不需要保存csvfile的所有结构信息，也可以直接使用CSVMapper进行序列化

```csharp
CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), content,1);
```

# 方法一和方法二的区别
当你想要保存CSVFile的完整结构，比如Header信息，以便今后还可以将csvfile保存成文件的时候，使用方法一

如果你不需要保存csvfile的所有结构信息，使用方法二

方法二比方法一少遍历一次

方法一因为需要构建CSVFile的信息，所以需要遍历一次，序列化时CSVMapper会再遍历一次。

如果不希望2次遍历，请使用方法二

