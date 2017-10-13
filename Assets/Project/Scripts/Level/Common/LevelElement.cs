/**********************************************
创建日期：2017/3/22 星期三 15:59:58
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

  public class LevelElement : LevelBehaviour
    {
        public float LifeTime = -1;

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Vector3 Scale
        {
            get { return transform.localScale; }
            set { transform.localScale = value; }
        }

        public Vector3 Euler
        {
            get { return transform.eulerAngles; }
            set { transform.eulerAngles = value; }
        }

        public virtual void SetName()
        {

        }

        public virtual void Build()
        {

        }

        public override void Init()
        {

        }

        public override LoadXML Export()
        {
            return null;
        }

        public override void Import(LoadXML pData, bool build)
        {

        }

        public override void Destroy()
        {

        }

        public static void GetAllComponents<T>(Transform trans, List<T> pList) where T : Component
        {
            if (trans == null) return;
            for (int i = 0; i < trans.childCount; i++)
            {
                T t = trans.GetChild(i).GetComponent<T>();
                if (t != null)
                {
                    pList.Add(t);
                }
            }
        }
    }
