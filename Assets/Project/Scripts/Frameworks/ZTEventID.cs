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
    REQ_PLAYER_DragEnemy    //请求玩家拖拽尸体
}