using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ActorRunFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        if (Cmd is RTCommand)
        {
            RTCommand ev = Cmd as RTCommand;
            Owner.OnPursue(ev);
        }
       
    }
}

