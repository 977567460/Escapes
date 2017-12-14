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
        public List<MapMonster> Monsters = new List<MapMonster>();
        public List<MapPlayer> Players = new List<MapPlayer>();
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
                    case "MonsterGroups":
                        LoadXML.GetChilds(current).ForEach(delegate(XmlNode pNode)
                        {
                            MapMonster data = new MapMonster();
                            data.Read(pNode);
                            this.Monsters.Add(data);
                        });
                        break;
                    case "PlayerGroups":
                        LoadXML.GetChilds(current).ForEach(delegate(XmlNode pNode)
                        {
                            MapPlayer data = new MapPlayer();
                            data.Read(pNode);
                            this.Players.Add(data);
                        });
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
            LoadXML.Write(os, "MonsterGroups", Monsters);
            LoadXML.Write(os, "PlayerGroups", Players);
        }
    }
}

