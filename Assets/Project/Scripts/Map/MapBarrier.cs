/**********************************************
创建日期：2017/3/22 星期三 16:41:17
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class MapBarrier : MapElement
{
    public float Width;
    public MapTransform TransParam;

    public override void Read(XmlNode os)
    {
        foreach (XmlNode current in LoadXML.GetChilds(os))
        {
            switch (current.Name)
            {
                case "Id":
                    this.Id = ReadInt(current);
                    break;
                case "TransParam":
                    this.TransParam = new MapTransform();
                    this.TransParam.Read(current);
                    break;
            }
        }
    }

    public override void Write(TextWriter os)
    {
        LoadXML.Write(os, "Id", (int)this.Id);
        LoadXML.Write(os, "TransParam", this.TransParam);
    }
}
