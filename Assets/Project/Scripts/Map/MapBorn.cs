/**********************************************
创建日期：2017/3/22 星期三 17:02:47
作者：张海城
说明:
**********************************************/
using System.IO;
using System.Xml;

public class MapBorn : LoadXML
{
    public EBattleCamp Camp;
    public MapTransform TransParam;

    public override void Read(XmlNode os)
    {
        foreach (XmlNode current in LoadXML.GetChilds(os))
        {
            switch (current.Name)
            {
                case "Camp":
                    this.Camp = (EBattleCamp)ReadInt(current);
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
        LoadXML.Write(os, "Camp", (int)this.Camp);
        LoadXML.Write(os, "TransParam", this.TransParam);
    }
}