using UnityEngine;
using System.Collections;

public enum EventID : ushort
{
    REQ_PLAYER_JUMP,          //请求玩家跳跃
    REQ_PLAYER_LEFT,
    REQ_PLAYER_RIGHT,
    REQ_PLAYER_FORWARD,
    REQ_PLAYER_BACKWARD
}