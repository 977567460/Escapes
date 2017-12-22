/**********************************************
创建日期：2017/12/19 星期二 19:03:44
作者：张海城
说明:
**********************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;


public class LevelItem:LoadXML
{
    public int sceneid;
    public int star;
    public bool isopen;
    public float passtime;
    public override void Read(XmlNode os)
    {
        foreach (XmlNode current in LoadXML.GetChilds(os))
        {
            switch (current.Name)
            {
                case "Id":
                    this.sceneid = ReadInt(current);
                    break;
                case "Star":
                    this.star = ReadInt(current);
                    break;
                case "IsOpen":
                    this.isopen = ReadBool(current);
                    break;
                case "PassTime":
                    this.passtime = ReadFloat(current);
                    break;

            }
     
        }
    }
    public override void Write(TextWriter os)
    {
        LoadXML.Write(os, "Id", this.sceneid);
        LoadXML.Write(os, "Star", this.star);
        LoadXML.Write(os, "IsOpen", this.isopen);
        LoadXML.Write(os, "PassTime", this.passtime);

    }
}

