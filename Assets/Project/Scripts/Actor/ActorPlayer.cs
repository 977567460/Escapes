using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ActorPlayer : Actor
{
    public ActorPlayer(int id, int guid, EActorType type, EBattleCamp camp)
        : base(id, guid, type, camp)
    {

    }

    public override void OnBeginRide()
    {
  
    }

    public override void OnEndRide()
    {
   
    }
    
}