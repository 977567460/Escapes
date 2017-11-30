using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    public class LevelMonster : LevelElement
    {     
        [SerializeField]
        public List<string> Talks = new List<string>();

        private GameObject mBody;

        public override void Build()
        {
            if (mBody == null)
            {
                mBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mBody.transform.parent = transform;
                mBody.transform.localPosition = Vector3.zero;
                mBody.transform.localEulerAngles = Vector3.zero;
                mBody.transform.localScale = Vector3.one;
            }
            MeshRenderer render = mBody.GetComponent<MeshRenderer>();
            if (render == null)
            {
                return;

            }
            if (render.sharedMaterial != null)
            {
                Shader shader = Shader.Find("Custom/TranspUnlit");
                render.sharedMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
            }
            render.sharedMaterial.color = Color.cyan;
        }

        public override void SetName()
        {
            gameObject.name = "Monster_" + Id.ToString();
        }

        public override void Import(LoadXML pData, bool pBuild)
        {
            Cfg.Map.MapMonster data = pData as Cfg.Map.MapMonster;
            Id = data.Id;
            Position = data.Position;
            Euler = data.Euler;
            Talks = data.Talks;
            Scale = data.Scale;           
            GameObject go = new GameObject();
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localEulerAngles = Vector3.zero;             
            
        }

        public override LoadXML Export()
        {
            Cfg.Map.MapMonster data = new Cfg.Map.MapMonster();
            data.Id = Id;
            data.Position = Position;
            data.Euler = Euler;
            data.Talks = Talks;
            data.Scale = Scale;          
            return data;
        }
    }

