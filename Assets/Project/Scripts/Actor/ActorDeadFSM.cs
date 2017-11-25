using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ActorDeadFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        Owner.OnDead();
    }
}

