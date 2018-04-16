using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIChaseState : AIBaseState
{
    public override void Enter()
    {
        Owner.TalkOther(50f);
    }

    public override void Execute()
    {
        if (Owner.GetTarget() != null)
        {
            Owner.Command(new RTCommand(Owner.GetTarget()));
            Owner.mActorPathFinding.SetStopDis(2);
            
        }
        switch (Owner.ActorType)
        {
            case EActorType.MONSTER:
                {
                    if (Owner.GetTarget() == null)
                    {
                        return;
                    }
                    float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetTarget().Pos);
                    if (dist > AI.WARDIST)
                    {
                        AI.ChangeAIState(EAIState.AI_BACK);                        
                    }
                    else if (dist < AI.ATKDIST && Owner.AiConeDetection.IsEnter)
                    {             
                        AI.ChangeAIState(EAIState.AI_FIGHT);
                        return;
                    }                
                }
                break;          
            case EActorType.PLAYER:
                {
                    if (Owner.GetTarget() == null)
                    {
                        AI.ChangeAIState(EAIState.AI_IDLE);
                        return;
                    }
                    float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetTarget().Pos);

                    if (dist < AI.ATKDIST)
                    {
                        AI.ChangeAIState(EAIState.AI_FIGHT);
                        return;
                    }
                }
                break;
        }
        

    }
    public override void Exit()
    {

    }
 
}
