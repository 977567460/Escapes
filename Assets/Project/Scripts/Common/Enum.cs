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
public enum EConditionRelation
{
    AND = 0,
    OR = 1,
}
public enum FSMState : int
{
    FSM_EMPTY,
    FSM_BORN,                //出生
    FSM_IDLE,                //待机
    FSM_TURN,                //转向

    FSM_WALK,                //漫步
    FSM_RUN,                 //跑

    FSM_SKILL,               //攻击
    FSM_DEAD,                //死亡
    FSM_REBORN,              //重生
    FSM_Attack,
    //FSM_WOUND,               //受击
    //FSM_BEATBACK,            //击退
    //FSM_BEATDOWN,            //击倒
    //FSM_BEATFLY,             //击飞
    //FSM_FLOATING,            //浮空

    //FSM_FROST,               //冰冻
    //FSM_STUN,                //昏迷
    //FSM_FIXBODY,             //定身
    //FSM_VARIATION,           //变形
    //FSM_FEAR,                //恐惧
    //FSM_SLEEP,               //睡眠
    //FSM_PARALY,              //麻痹
    //FSM_BLIND,               //致盲

    FSM_PICK,                //捡起

    //FSM_RIDEIDLE,            //骑乘闲置
    //FSM_RIDERUN,             //骑乘跑

    FSM_DROP,                //下落
    FSM_TALK,                //说话
    FSM_HOOK,                //钩子
    FSM_GRAB,                //抓取
    FSM_FLY,                 //飞行
    FSM_RAGDOLL,             //布娃娃
    FSM_ROLL,                //翻滚
    FSM_JUMP,                //跳跃

    FSM_DANCE,               //跳舞
    FSM_MINE,                //采集状态
    FSM_INTERACTIVE,         //交互
}
public enum EActorType
{
    PLAYER,   //玩家
    MONSTER,  //怪物

}
public enum EMonsterType
{
    PLAYER,   //玩家
    People,  //群众
    Patroler,//巡逻人员
    Sniper//狙击手

}
public enum ETargetCamp
{
    None,
    Ally,
    Enemy,
    Neutral,
}
public enum Language
{
    Chinese,
    English
}
public enum EProperty
{
    LHP = 1,   //生命值
    ATK = 2,   //攻击力

}
public enum EAttr
{
    MaxHP = 1,   //最大生命值  
    HP = 2,   //生命值
    Atk = 3,   //攻击力
    Speed = 4,   //速度
    StartAngle = 5,//开始角度
    EndAngle = 6,//结束角度
    ViewLength = 7,//视野长度
    WaitPatrolTime=8,//等待时间
}
public enum EAIState
{
    AI_NONE,  //无
    AI_IDLE,  //闲逛
    AI_FIGHT, //战斗
    AI_PATROL,//巡逻
    AI_DEAD,  //死亡
    AI_BACK,  //回家
    AI_CHASE, //追击
    AI_FLEE,  //避开
    AI_ESCAPE,//逃跑
    AI_BORN,  //出生
    AI_PLOT,  //剧情
    AI_GLOBAL,//全局
}
public enum EAIMode
{
    Auto,      //自动
    Hand,      //手动
}
public enum EActorEffect
{
    IS_AutoToMove,
    IS_Task,
    IS_Story,
    IS_Stealth,
    Is_Silent,
    Is_Divine,
    Is_Super,
    Is_Ride,
}