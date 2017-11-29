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
        ZTEvent.AddHandler(EventID.REQ_PLAYER_JUMP, OnJump);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_LEFT, OnLeft);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_BACKWARD, OnBack);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_FORWARD, OnForward);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_RIGHT, OnRight);
    }
    void OnJump()
    {
        this.SendStateMessage(FSMState.FSM_JUMP);
      
    }
    void OnLeft()
    {
        this.SendStateMessage(FSMState.FSM_WALK);
    }
    void OnRight()
    {
        this.SendStateMessage(FSMState.FSM_WALK);
    }
    void OnBack()
    {
        this.SendStateMessage(FSMState.FSM_WALK);
    }
    void OnForward()
    {
        this.SendStateMessage(FSMState.FSM_WALK);      
    }
    public override void Destroy()
    {
        base.Destroy();
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_JUMP, OnJump);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_LEFT, OnLeft);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_BACKWARD, OnBack);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_FORWARD, OnForward);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_RIGHT, OnRight);
    }
}

