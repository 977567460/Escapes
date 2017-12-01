using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ActorMainPlayer : ActorPlayer
{
    private float speed = 5;
    public ActorMainPlayer(int id, int guid, EActorType type, EBattleCamp camp)
        : base(id, guid, type, camp)
    {

    }
    public override void Init()
    {
        base.Init();
        ZTEvent.AddHandler(EventID.REQ_PLAYER_JUMP, OnMainPlayerJump);
        ZTEvent.AddHandler<float,float>(EventID.REQ_PLAYER_Walk, OnMainPlayerWalk);
   
    }
    void OnMainPlayerJump()
    {
        this.SendStateMessage(FSMState.FSM_JUMP);
      
    }
    void OnMainPlayerWalk(float arg1, float arg2)
    {
        Vector2 delta = new Vector2(arg1, arg2);
        this.SendStateMessage(FSMState.FSM_WALK, new MVCommand(delta));
    }

    public override void Destroy()
    {
        base.Destroy();
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_JUMP, OnMainPlayerJump);
        ZTEvent.RemoveHandler<float,float>(EventID.REQ_PLAYER_Walk, OnMainPlayerWalk);
    
    }
}

