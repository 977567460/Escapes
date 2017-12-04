﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace Cfg.Map
{
    public class MapMonster : MapElement
    {
        public Vector3 Position;
        public Vector3 Euler;
        public Vector3 Scale = Vector3.one;
        public List<string> Talks = new List<string>();

        public override void Read(XmlNode os)
        {
            foreach (XmlNode current in LoadXML.GetChilds(os))
            {
                switch (current.Name)
                {
                    case "Id":
                        this.Id = ReadInt(current);
                        break;
                    case "Position":
                        this.Position = ReadVector3(current);
                        break;
                    case "Euler":
                        this.Euler = ReadVector3(current);
                        break;
                    case "Scale":
                        this.Scale = ReadVector3(current);
                        break;
                    case "Talks":
                        LoadXML.GetChilds(current).ForEach(delegate(XmlNode pNode)
                        {
                            string s = ReadString(pNode);
                            if (!string.IsNullOrEmpty(s))
                            {
                                this.Talks.Add(s);
                            }
                        });
                        break;                 
                }
            }
        }

        public override void Write(TextWriter os)
        {
            LoadXML.Write(os, "Id", this.Id);
            LoadXML.Write(os, "Position", this.Position);
            LoadXML.Write(os, "Euler", this.Euler);
            LoadXML.Write(os, "Scale", this.Scale);
            LoadXML.Write(os, "Talks", this.Talks);
        }
    }
}
