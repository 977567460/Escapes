using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ActorMainPlayer : ActorPlayer
{
    public ActorMainPlayer(int id, int guid, EActorType type, EBattleCamp camp, List<Vector3> PatrolGroups)
        : base(id, guid, type, camp, PatrolGroups)
    {

    }
    public override void Init()
    {
        base.Init();
        //ZTEvent.AddHandler(EventID.REQ_PLAYER_JUMP, OnMainPlayerJump);
        //ZTEvent.AddHandler<float,float>(EventID.REQ_PLAYER_Walk, OnMainPlayerWalk);
        //ZTEvent.AddHandler(EventID.REQ_PLAYER_Idle, OnMainPlayerIdle);
        //ZTEvent.AddHandler(EventID.REQ_PLAYER_Attack, OnMainPlayerAttack);
        //ZTEvent.AddHandler(EventID.REQ_PLAYER_Change, SetMainPlayer);       
    }
    void OnMainPlayerJump()
    {
        this.SendStateMessage(FSMState.FSM_JUMP);
      
    }
    void OnMainPlayerAttack()
    {
        this.SendStateMessage(FSMState.FSM_Attack);
    }
    void OnMainPlayerWalk(float arg1, float arg2)
    {
        Vector2 delta = new Vector2(arg1, arg2);
        this.SendStateMessage(FSMState.FSM_WALK, new MVCommand(delta));
    }
    void OnMainPlayerIdle()
    {
        this.SendStateMessage(FSMState.FSM_IDLE);
    }

    void SetMainPlayer()
    {
        if (LevelData.MainPlayer.Id == 1)
        {
            LevelManage.Instance.SetMainPlayer(2);
        }       
        else
        {
            LevelManage.Instance.SetMainPlayer(1);
        }
    }
    public void addMainPlayer()
    {
  
        ZTEvent.AddHandler(EventID.REQ_PLAYER_JUMP, OnMainPlayerJump);
        ZTEvent.AddHandler<float, float>(EventID.REQ_PLAYER_Walk, OnMainPlayerWalk);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_Idle, OnMainPlayerIdle);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_Attack, OnMainPlayerAttack);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_Change, SetMainPlayer);
    }
    public void RemoveMainPlayer()
    {
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_JUMP, OnMainPlayerJump);
        ZTEvent.RemoveHandler<float, float>(EventID.REQ_PLAYER_Walk, OnMainPlayerWalk);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_Idle, OnMainPlayerIdle);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_Attack, OnMainPlayerAttack);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_Change, SetMainPlayer);   
    }
    public override void Destroy()
    {
        base.Destroy();
        if(LevelData.MainPlayer==this)
        RemoveMainPlayer();
    }

    public override void OnDead()
    {
        base.OnDead();
    }

   
}

