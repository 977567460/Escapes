/**********************************************
创建日期：2017/3/22 星期三 16:58:37
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum EBattleCamp
{
    A,//我方
    B,//敌方
    C,//中立
    D,//其他
}
public enum EMapTrigger
{
    TYPE_NONE,          //什么也不做
    TYPE_WAVESET = 1,   //触发怪物波次
    TYPE_TASK = 2,    //触发任务
    TYPE_PLOT = 3,      //触发剧情
    TYPE_MACHINE = 4,   //触发机关
    TYPE_BARRIER = 5,   //触发光墙  
    TYPE_REGION = 6,    //触发新的触发器
    TYPE_RESULT = 7,    //触发副本结算
    TYPE_CUTSCENE = 8,  //触发过场动画
    TYPE_PORTAL = 9,    //触发一个传送门
    TYPE_BUFF = 10,     //触发Buff、光环
    TYPE_MONSTEGROUP = 11, //触发怪物堆
    TYPE_MINEGROUP = 12,//触发矿石堆
}
public enum EConditionRelation
{
    AND = 0,
    OR = 1,
}
