using UnityEngine;
using System.Collections;

public enum EventID : ushort
{
    REQ_PLAYER_JUMP,  //请求玩家跳跃
    REQ_PLAYER_Walk,  //请求玩家行走
    REQ_PLAYER_Idle,  //请求玩家站立
    REQ_PLAYER_Attack,  //请求玩家攻击
    REQ_PLAYER_Change, //请求玩家切换角色
    REQ_PLAYER_Attr,    //请求玩家属性改变
    REQ_PLAYER_TakeStone,   //请求玩家拿石头
    REQ_PLAYER_ThrowingStone,   //请求玩家扔石头
    REQ_PLAYER_EnemyArea   //请求玩家视野范围
}