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
        public float Delay;
        public string MapName = string.Empty;
        public string MapPath = string.Empty;
        public bool AllowRide = true;
        public bool AllowPK = true;
        public bool AllowTrade = true;
        public bool AllowFight = true;
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
                    case "Delay":
                        this.Delay = ReadFloat(current);
                        break;
                    case "MapName":
                        this.MapName = ReadString(current);
                        break;
                    case "MapPath":
                        this.MapPath = ReadString(current);
                        break;
                    case "AllowRide":
                        this.AllowRide = ReadBool(current);
                        break;
                    case "AllowPK":
                        this.AllowPK = ReadBool(current);
                        break;
                    case "AllowTrade":
                        this.AllowTrade = ReadBool(current);
                        break;
                    case "AllowFight":
                        this.AllowFight = ReadBool(current);
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
            LoadXML.Write(os, "Delay", Delay);
            LoadXML.Write(os, "MapName", MapName);
            LoadXML.Write(os, "MapPath", MapPath);
            LoadXML.Write(os, "AllowRide", AllowRide);
            LoadXML.Write(os, "AllowPK", AllowPK);
            LoadXML.Write(os, "AllowTrade", AllowTrade);
            LoadXML.Write(os, "AllowFight", AllowFight);
            LoadXML.Write(os, "A", A);
            LoadXML.Write(os, "B", B);
            LoadXML.Write(os, "C", C);                
        }
    }
}

