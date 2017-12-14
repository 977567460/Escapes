using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ActorDeadFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        Owner.OnDead();        
    }
}

