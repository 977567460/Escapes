using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ActorPlayer : Actor
{
    public ActorPlayer(int id, int guid, EActorType type, EBattleCamp camp, List<Vector3> PatrolGroups)
        : base(id, guid, type, camp,PatrolGroups)
    {

    }

    public override void OnBeginRide()
    {
  
    }

    public override void OnEndRide()
    {
   
    }
    public override void OnDead()
    {
        base.OnDead();
        LevelManage.Instance.OnBattleEnd();
    }
    
}