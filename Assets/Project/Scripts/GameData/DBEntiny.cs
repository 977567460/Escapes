/**********************************************
创建日期：2017/11/24 星期五 11:24:16
作者：张海城
说明:
**********************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using UnityEngine;

public class DBEntiny : DBModule
{
    public int Id;
    public int Level;
    public EActorType Type; 
    public string Name = string.Empty;
    public string Ctrl = string.Empty;
    public string Model = string.Empty;
    public string Desc = string.Empty;
    public string Title = string.Empty;
    public string Icon = string.Empty;
    public int Group;
    public int Quality;
    public int Exp;
    public float WSpeed;
    public float RSpeed;
    public int BornEffectID;
    public int DeadEffectID;
    public Vector3 StagePos;
    public float StageScale;
    public string Voice = string.Empty;
    public string AIFeature = string.Empty;
    public string AIScript = string.Empty;
    public int MaxHp;
    public int Attack;
    public Dictionary<EProperty, int> Propertys = new Dictionary<EProperty, int>();
    public override int GetTypeId()
    {
        return Id;
    }
}
public class ReadDBEntiny : IReadConfig<int, DBEntiny>
{
    protected override void LoadData(Dictionary<int, DBEntiny> dict)
    {

        FileStream stream = File.Open(Application.dataPath + "/Project/xlsx/Entiny.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;
        for (int i = 1; i < rows; i++)
        {
            DBEntiny db = new DBEntiny();
            db.Id = int.Parse(result.Tables[0].Rows[i][0].ToString());
            db.Name = result.Tables[0].Rows[i][1].ToString();
            db.Title = result.Tables[0].Rows[i][2].ToString();
            db.Level =int.Parse(result.Tables[0].Rows[i][3].ToString());
            db.Type = (EActorType)int.Parse(result.Tables[0].Rows[i][4].ToString());
            db.Icon = result.Tables[0].Rows[i][5].ToString();
            db.WSpeed = Convert.ToSingle(result.Tables[0].Rows[i][6].ToString());
            db.RSpeed = Convert.ToSingle(result.Tables[0].Rows[i][7].ToString());           
            db.BornEffectID =int.Parse(result.Tables[0].Rows[i][8].ToString());
            db.DeadEffectID = int.Parse(result.Tables[0].Rows[i][9].ToString());
            db.Model = result.Tables[0].Rows[i][10].ToString();
            db.Ctrl = result.Tables[0].Rows[i][11].ToString();
            db.AIScript = result.Tables[0].Rows[i][12].ToString();
            db.Desc = result.Tables[0].Rows[i][13].ToString();
            db.MaxHp =int.Parse( result.Tables[0].Rows[i][14].ToString());
            db.Attack = int.Parse(result.Tables[0].Rows[i][15].ToString());
            if (!dict.ContainsKey(db.Id))
            {
                dict.Add(db.Id, db);
            }
            db.Propertys.Add(EProperty.ATK, db.Attack);
            db.Propertys.Add(EProperty.LHP, db.MaxHp);      
        }
       
    }
}

