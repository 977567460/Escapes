using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public   class ActorMainPlayer:Actor
{
    public ActorMainPlayer(int id, int guid)
          : base(id, guid)
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
        this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_JUMP));
    }
    void OnLeft()
    {
        this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_WALK));
    }
    void OnRight()
    {
        this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_WALK));
    }
    void OnBack()
    {
        this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_WALK));
    }
    void OnForward()
    {
        this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_WALK));
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

