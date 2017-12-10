using UnityEngine;
using System.Collections;

public class AIIdleState : AIBaseState
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
                        if (Owner.mActorPathFinding.AiConeDetection.IsEnter)
                        {
                           
                            AI.ChangeAIState(EAIState.AI_CHASE);
                        }
                    }
                    if (Owner.MonsterType == EMonsterType.Patroler)
                    {
                        AI.ChangeAIState(EAIState.AI_PATROL);
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
