/**********************************************
创建日期：2017/11/24 星期五 10:56:50
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Actor : ICharacter
{
      protected IStateMachine<Actor, FSMState> mMachine;
      public ZTAction mActorAction;
      protected Animator mActorAnimator;
     
      public Actor(int id, int guid)
          : base(id, guid)
      {        
         
      }
      public override void Init()
      {
          InitAction();
          InitFSM();
      }

      protected void InitFSM()
      {
          this.mMachine = new IStateMachine<Actor, FSMState>(this);
          this.mMachine.AddState(FSMState.FSM_EMPTY, new ActorEmptyFSM());
          this.mMachine.AddState(FSMState.FSM_IDLE, new ActorIdleFSM());
          this.mMachine.AddState(FSMState.FSM_RUN, new ActorRunFSM());
          this.mMachine.AddState(FSMState.FSM_WALK, new ActorWalkFSM());      
          this.mMachine.AddState(FSMState.FSM_DEAD, new ActorDeadFSM());
          this.mMachine.AddState(FSMState.FSM_JUMP, new ActorJumpFSM());
          this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_IDLE));
          this.mMachine.GetState(this.mMachine.GetCurrStateID()).Enter();
      }
      public void InitAction()
      {
          DBEntiny db = GameDataManage.Instance.GetDBEntiny(Id);
          if (db == null)
          {
              return;
          }
          mActorAnimator = GTTools.LoadAnimator(Obj, db.Ctrl);
          if (mActorAnimator != null)
          {
              mActorAnimator.enabled = true;
              mActorAnimator.applyRootMotion = true;
              mActorAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
              this.mActorAction = new ZTAction(mActorAnimator);
          }
      }

      public virtual void OnWalk()
      {

          this.mActorAction.Play("Walk", null, true);
      }
      public virtual void OnIdle()
      {
          this.mActorAction.Play("Idle", null, true);
      }
      public virtual void OnRun()
      {
          this.mActorAction.Play("Run", null, true);
      }
      public virtual void OnDead()
      {
          this.mActorAction.Play("Dead");
      }
      public virtual void OnJump()
      {
          this.mActorAction.Play("Jump");
      }
      public override void Destroy()
      {
          throw new NotImplementedException();
      }

      public override void Clear()
      {
          throw new NotImplementedException();
      }

      public override void Step()
      {
          throw new NotImplementedException();
      }

      public override void ChangeModel(int modelID)
      {
          throw new NotImplementedException();
      }

      public override bool IsDead()
      {
          throw new NotImplementedException();
      }

      public override bool IsDestroy()
      {
          throw new NotImplementedException();
      }

      public override void Pause(bool pause)
      {
          throw new NotImplementedException();
      }
}

