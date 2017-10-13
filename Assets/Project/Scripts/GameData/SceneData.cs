using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Excel;
using System.Data;


public enum ESceneType
{
    Init,
    Login,
    Role,
    Battle,
  
}
public class SceneData :DBModule{
    public int Id;
    public string GateLevel;   
    public int StarNum;
    public bool IsOpen;
    public ESceneType SceneType;
    public string SceneName;
    public string SceneMusic;
   
    


    public override int GetTypeId()
    {
        return Id;
    }
}
public class ReadSceneData : IReadConfig<int, SceneData>
{ 
   protected override void LoadData(Dictionary<int, SceneData> dict)
   {     
    
       FileStream stream = File.Open(Application.dataPath + "/Project/xlsx/GateLevel.xlsx", FileMode.Open, FileAccess.Read);
       IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

       DataSet result = excelReader.AsDataSet();

       int columns = result.Tables[0].Columns.Count;
       int rows = result.Tables[0].Rows.Count;    
       for (int i = 1; i < rows; i++)
       {
           SceneData db = new SceneData();
           db.Id = int.Parse(result.Tables[0].Rows[i][0].ToString());
           db.SceneName = result.Tables[0].Rows[i][1].ToString();
           db.GateLevel = result.Tables[0].Rows[i][2].ToString();
           string Open = result.Tables[0].Rows[i][3].ToString();
           if (Open == "1")
           {
               db.IsOpen = true;
           }
           else
           {
               db.IsOpen = false;
               
           }
           //db.SceneType = (ESceneType)Enum(result.Tables[0].Rows[i][4].ToString());
           db.SceneType = (ESceneType)Enum.Parse(typeof(ESceneType), result.Tables[0].Rows[i][4].ToString());
           db.SceneName = result.Tables[0].Rows[i][5].ToString();
           db.SceneMusic = result.Tables[0].Rows[i][6].ToString();
           if (!dict.ContainsKey(db.Id))
           {
               dict.Add(db.Id, db);               
           }
       }
   }
}
