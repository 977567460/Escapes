/**********************************************
创建日期：2017/3/22 星期三 16:25:44
作者：张海城
说明:
**********************************************/
using System.IO;
using System.Xml;

public class MapElement : LoadXML
{
    public int Id;

    public override void Read(XmlNode os)
    {
        foreach (XmlNode current in LoadXML.GetChilds(os))
        {
            switch (current.Name)
            {
                case "Id":
                    this.Id = ReadInt(current);
                    break;
            }
        }
    }

    public override void Write(TextWriter os)
    {
        LoadXML.Write(os, "Id", this.Id);
    }
}
