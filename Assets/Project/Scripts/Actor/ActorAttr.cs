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
            default:
                break;
        }
    }

    public static ActorAttr operator +(ActorAttr a, ActorAttr b)
    {
        ActorAttr c = new ActorAttr();
        c.HP = a.HP + b.HP;
        c.MaxHP = a.MaxHP + b.MaxHP;
        c.Atk = a.Atk + b.Atk;
        c.Speed = a.Speed + b.Speed;

        return c;
    }


}
