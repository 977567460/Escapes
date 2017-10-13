/**********************************************
创建日期：2017/3/22 星期三 15:58:49
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class LevelBehaviour : MonoBehaviour
{
    public int Id { get; set; }
    public abstract void Init();
    public abstract void Destroy();
    public abstract void Import(LoadXML pdata, bool pBuild);
    public abstract LoadXML Export();
}

