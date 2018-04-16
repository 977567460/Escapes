using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class ActorAttr
{
    public int HP;
    public int MaxHP;
    public int Atk;
    public int Speed;
    public int ViewLength;
    public int StartAngle;
    public int EndAngle;
    public int WaitPatrolTime;
    public int GetAttr(EAttr property)
    {
        switch (property)
        {
            case EAttr.Atk:
                return Atk;
            case EAttr.HP:
                return HP;
            case EAttr.MaxHP:
                return MaxHP;
            case EAttr.Speed:
                return Speed;
            case EAttr.ViewLength:
                return ViewLength;
            case EAttr.StartAngle:
                return StartAngle;
            case EAttr.EndAngle:
                return EndAngle;
            case EAttr.WaitPatrolTime:
                return WaitPatrolTime;
            default:
                return 0;
        }
    }

    public void CopyFrom(Dictionary<EProperty, int> dict)
    {
        if (dict == null) return;
        this.Atk = dict[EProperty.ATK];
        this.MaxHP = dict[EProperty.LHP];
        this.HP = dict[EProperty.LHP];
    }

    public void Update(EAttr attr, int value)
    {
        switch (attr)
        {
            case EAttr.Atk:
                Atk = value;
                break;
            case EAttr.HP:
                HP = value;
                break;
            case EAttr.MaxHP:
                MaxHP = value;
                break;
            case EAttr.Speed:
                Speed = value;
                break;
            case EAttr.StartAngle:
                StartAngle = value;
                break;
            case EAttr.EndAngle:
                EndAngle = value;
                break;
            case EAttr.ViewLength:
                ViewLength = value;
                break;
            case EAttr.WaitPatrolTime:
                WaitPatrolTime = value;
                break;
            default:
                break;
        }
    }


}
