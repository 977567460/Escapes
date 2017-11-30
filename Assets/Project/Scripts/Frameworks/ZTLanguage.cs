using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTLanguage : MonoSingleton<ZTLanguage>
{
    public Dictionary<string,string> TalkDictionary=new Dictionary<string, string>();
    public Language _Language;
    private string FileName;
    private TextAsset pAsset;
	// Use this for initialization
	void Start () {
	    if (_Language == Language.Chinese)
	    {
	        FileName = "ChineseLanguage";
	    }
	    else
	    {
            FileName = "EnglishLanguage";
	    }
        string fsPath = GTTools.Format("Text/Language/{0}", FileName);
         pAsset = LoadResource.Instance.Load<TextAsset>(fsPath);
	    InitText();

	}
	
	// Update is called once per frame
	void Update () {
		
	} 
    public string ShowText(string key)
    {
        foreach (KeyValuePair <string,string> kv in TalkDictionary)
        {
            if (key == kv.Key)
                return kv.Value;      
        }
        return key;
    }

    void InitText()
    {
        string talk = pAsset.text;
        string[] talkCol = talk.Split('\n');
        for (int i = 0; i < talkCol.Length; i++)
        {
            string[] talkRow = talkCol[i].Split(',');
            TalkDictionary.Add(talkRow[0],talkRow[1]);
        }
       
    }
}
