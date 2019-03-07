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
        CSVFile file = new CSVFile();
        file.Parse(content);

        List<AddressBookData> list = CSVMapper.Map<AddressBookData>(new AddressBookDataParser(), file);
        for (int i = 0; i < list.Count; i++)
        {
            AddressBookData abd = list[i];
            Debug.Log(abd.Name);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}
}
