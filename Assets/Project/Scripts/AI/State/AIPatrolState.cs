using UnityEngine;
using System.Collections;

public class AIPatrolState : AIBaseState
{
    public override void Enter()
    {

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
                        float dist = GTTools.GetHorizontalDistance(Owner.Pos, pTarget.Pos);
                        if (dist < AI.WARDIST)
                        {
                            AI.ChangeAIState(EAIState.AI_CHASE);
                        }
                    }
                }
                break;
            case EActorType.PLAYER:
                {
                    if (pTarget != null)
                    {
                        float dist = GTTools.GetHorizontalDistance(Owner.Pos, pTarget.Pos);
                        if (dist < AI.WARDIST)
                        {
                            AI.ChangeAIState(EAIState.AI_CHASE);
                        }
                    }
                }
                break;
        }
    }

    public override void Exit()
    {

    }
}
