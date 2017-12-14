using UnityEngine;
using System.Collections;

public class AIFightState : AIBaseState
{
    private float SkillCD = 0.5f;
    private float Timerr = 0;
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
                    if (pTarget == null)
                    {
                        AI.ChangeAIState(EAIState.AI_BACK);
                        return;
                    }
                    Fight();
                }
                break;        
            case EActorType.PLAYER:
                {
                    if (pTarget == null)
                    {
                        AI.ChangeAIState(EAIState.AI_IDLE);
                        return;
                    }
                    Fight();
                }
                break;
        }


    }

    private void Fight()
    {
        float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetTarget().Pos);
        if (dist > AI.ATKDIST)
        {
            AI.ChangeAIState(EAIState.AI_CHASE);
            return;
        }
        if(Owner.FSM==FSMState.FSM_SKILL)
        {
            return;
        }
        Owner.SendStateMessage(FSMState.FSM_Attack);
        Timerr += Time.deltaTime;
        if (Timerr >= SkillCD)
        {
            GameObject Bullet = ZTPool.Instance.GetGo("Model/Weapons/bullet");           
            Bullet.transform.position = Owner.BulletOrigin.position;
            Bullet.transform.eulerAngles = Owner.BulletOrigin.eulerAngles;
            Timerr = 0;
        }
       
    }

    public override void Exit()
    {

    }
}
