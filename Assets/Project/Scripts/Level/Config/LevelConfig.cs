/**********************************************
创建日期：2017/12/19 星期二 17:49:03
作者：张海城
说明:
**********************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;


public class LevelConfig : LoadXML
{
    public  List<LevelItem> SceneGroups=new List<LevelItem>();

    public override void Read(XmlNode os)
    {
        foreach (XmlNode current in LoadXML.GetChilds(os))
        {
            switch (current.Name)
            {
                case "SceneGroups":
                    LoadXML.GetChilds(current).ForEach(delegate(XmlNode pNode)
                    {
                        LevelItem data = new LevelItem();
                        data.Read(pNode);
                        this.SceneGroups.Add(data);
                    });
                    break;         
         
            }
        }
    }
    public override void Write(TextWriter os)
    {
        LoadXML.Write(os, "SceneGroups", SceneGroups);
    
    }
}

