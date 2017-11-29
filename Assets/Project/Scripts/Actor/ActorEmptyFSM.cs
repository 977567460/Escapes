using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ActorEmptyFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        Owner.SendStateMessage(FSMState.FSM_IDLE);
    }
}

