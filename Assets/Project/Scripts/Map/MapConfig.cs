using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace Cfg.Map
{
    public class MapConfig : LoadXML
    {
        public int Id;
        public MapBorn A;
        public MapBorn B;
        public MapBorn C;
        public override void Read(XmlNode os)
        {
            foreach (XmlNode current in LoadXML.GetChilds(os))
            {
                switch (current.Name)
                {
                    case "Id":
                        this.Id = ReadInt(current);
                        break;
                    case "A":
                        this.A = new MapBorn();
                        this.A.Read(current);
                        break;
                    case "B":
                        this.B = new MapBorn();
                        this.B.Read(current);
                        break;
                    case "C":
                        this.C = new MapBorn();
                        this.C.Read(current);
                        break;


                }
            }
        }

        public override void Write(TextWriter os)
        {
            LoadXML.Write(os, "Id", Id);
            LoadXML.Write(os, "A", A);
            LoadXML.Write(os, "B", B);
            LoadXML.Write(os, "C", C);
        }
    }
}

