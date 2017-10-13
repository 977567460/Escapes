/**********************************************
创建日期：2017/3/22 星期三 16:03:01
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelContainerBase<T> : LevelElement where T : LevelElement
{
    public List<T> Elements
    {
        get
        {
            mList.Clear();
            GetAllComponents<T>(transform, mList);
            return mList;
        }
    }

    public virtual T AddElement()
    {
        T pElem = new GameObject().AddComponent<T>();
        pElem.transform.parent = transform;
        pElem.Build();
        pElem.SetName();
        return pElem;
    }

    public T FindElement(int id)
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            if (Elements[i].Id == id)
            {
                return Elements[i];
            }
        }
        return null;
    }

    private List<T> mList = new List<T>();
}
