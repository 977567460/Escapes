/**********************************************
创建日期：2017/3/22 星期三 16:43:48
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;


public class MapTransform : LoadXML
{
    public Vector3 Position = Vector3.zero;
    public Vector3 EulerAngles = Vector3.zero;
    public Vector3 Scale = Vector3.one;

    public override void Read(XmlNode os)
    {
        foreach (XmlNode current in LoadXML.GetChilds(os))
        {
            switch (current.Name)
            {
                case "Position":
                    this.Position = ReadVector3(current);
                    break;
                case "EulerAngles":
                    this.EulerAngles = ReadVector3(current);
                    break;
                case "Scale":
                    this.Scale = ReadVector3(current);
                    break;
            }
        }
    }

    public override void Write(TextWriter os)
    {
        LoadXML.Write(os, "Position", this.Position);
        LoadXML.Write(os, "EulerAngles", this.EulerAngles);
        LoadXML.Write(os, "Scale", this.Scale);
    }
}
