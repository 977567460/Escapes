/**********************************************
创建日期：2017/12/14 星期四 10:16:21
作者：张海城
说明:
**********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ActorPart : IGame
{
    public Transform AttackTransform;
    public Transform AIConeDetection;
    public Transform TalkTransform;
    public Actor owner;
    public ActorPart(Actor owner)
    {
        this.owner = owner;
        this.AIConeDetection = owner.Obj.transform.Find("AIDetection");
        this.TalkTransform = this.owner.Obj.transform.Find("Talk");
        if (owner.ActorType == EActorType.MONSTER)
        {
            this.AttackTransform = this.owner.Obj.transform.Find("Hips_jnt/Spine_jnt/Spine_jnt 1/Chest_jnt/Shoulder_Right_jnt/Arm_Right_jnt/Forearm_Right_jnt/Hand_Right_jnt/SA_Wep_AssaultRifle01/Bullet");
            
        }
    }

    public void Start()
    {

    }

    public void Step()
    {

    }

    public void Clear()
    {

    }
}

