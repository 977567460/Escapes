using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ActorWalkFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        MVCommand ev = Cmd as MVCommand;
        Owner.OnForceToMove(ev);

    }
}

