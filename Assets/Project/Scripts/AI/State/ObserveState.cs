using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public  class ObserveState: AIBaseState
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void Execute()
    {
        Actor pTarget = Owner.GetTarget();
        switch (Owner.ActorType)
        {
            case EActorType.MONSTER:
                {
                    if (pTarget != null)
                    {                        
                        if (Owner.AiConeDetection.IsEnter)
                        {
                            AI.ChangeAIState(EAIState.AI_FIGHT);
                        }
                    }
                    Observer();
                }
                break;

        }
    }
    public void Observer(){
        Owner.CacheTransform.transform.Rotate(Vector3.up, 30);
    }
}

