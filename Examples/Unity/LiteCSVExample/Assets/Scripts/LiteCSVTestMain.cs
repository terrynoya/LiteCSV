using System.Collections;
using System.Collections.Generic;
using LiteCSV;
using UnityEngine;

public class LiteCSVTestMain : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        string path = "test.CSV";
        TextAsset txtAssest = Resources.Load<TextAsset>(path);
        string content = txtAssest.text;
	    int dataStartLineNumber = 1;
        CSVFile file = new CSVFile(dataStartLineNumber);
        file.Parse(content);

        List<AddressBookData> list = CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), file);
	    AddressBookData data = list[0];
	    data.Name = "modify";
        AdressBookDataWriter writer = new AdressBookDataWriter();
	    CSVLineData lineToModify = file.GetLineDataAt(0);
        writer.SetData(data, lineToModify);
	    string finalCSVText = file.ToCSV();
        
        CSVMapper.ToCSV(new AdressBookDataWriter(),file,list);
	    finalCSVText = file.ToCSV();
        Debug.Log(finalCSVText);

	    //for (int i = 0; i < list.Count; i++)
	    //{
	    //    AddressBookData abd = list[i];
	    //    Debug.Log(abd.Name);
	    //}
	    //CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), content,1);


    }

    // Update is called once per frame
    void Update ()
    {
	    	
	}
}
