using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPatrolState : AIBaseState
{
    private List<Vector3> PatrolGroups = new List<Vector3>();
    private float ThinkingTime;
    private float Timerr=0;
    private bool Iswalking = false;
    private Vector3 PreTarget =Vector3.zero;
    public override void Enter()
    {
        PatrolGroups = Owner.PatrolGroups;
        ThinkingTime = Owner.GetAttr(EAttr.WaitPatrolTime);
     
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
                    Patrol();
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
                    Patrol();
                }
                break;
        }
    }

    public override void Exit()
    {

    }
    void Patrol()
    {
        if(!Iswalking)
        Timerr += Time.deltaTime;
        if (Timerr >= ThinkingTime)
        {       
            Vector3 target = PatrolGroups[Random.Range(0, PatrolGroups.Count)];
            if (PreTarget == target) return;
            Iswalking = true;
            Timerr = 0;
            Owner.mActorPathFinding.SetDestPosition(target);
            Owner.mActorPathFinding.SetOnFinished(()=>{
                Iswalking = false;
            });
            Owner.SendStateMessage(FSMState.FSM_WALK);
            PreTarget = target;
        }      
    }
  
}
