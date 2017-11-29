using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorBaseFSM : IState<Actor, FSMState>
{


    public override void Enter()
    {

    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
       
    }

    protected virtual void Break()
    {
        Owner.SendStateMessage(FSMState.FSM_EMPTY);
    }

    protected void Update()
    {

    }
}
