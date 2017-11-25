using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class ActorIdleFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        Owner.OnIdle();
    }
}

