﻿/**********************************************
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
    protected ActorBehavior mBehavior;
    protected XTransform mBornParam;
    public EActorType ActorType { get; private set; }
    public EBattleCamp Camp { get; set; }
    public Actor(int id, int guid, EActorType type, EBattleCamp camp)
        : base(id, guid)
    {
        this.ActorType = type;
        this.Camp = camp;
    }
    public override void Init()
    {
        InitAction();
        InitFSM();
        InitBehavior();
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
    protected void InitBehavior()
    {
        this.mBehavior = Obj.GET<ActorBehavior>();
        this.mBehavior.SetOwner(this);
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
    public virtual void OnBeginRide()
    {

    }

    public virtual void OnEndRide()
    {

    }
    public override void Load(XTransform data)
    {
        this.Obj = LoadObject(data);
        if (this.Obj == null)
        {
            return;
        }
        this.CacheTransform = Obj.transform;
        this.mBornParam = data;

        this.Init();
    }
    public GameObject LoadObject(XTransform data)
    {   
        DBEntiny db = GameDataManage.Instance.GetDBEntiny(Id);
        if (db != null)
        {
            GameObject go = ZTPool.Instance.GetGo(db.Model);
            go.transform.localPosition = data.Position;
            go.transform.localEulerAngles = data.EulerAngles;
            if (data.Scale != Vector3.zero)
            {
                go.transform.localScale = data.Scale;
            }
            return go;
        }
        return null;
    }
    public override void Destroy()
    {
        mActorAction.Clear();
        mMachine.Clear();  
    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }

    public override void Step()
    {
        if (CacheTransform == null || mMachine == null)
        {
            return;
        }

        mMachine.Step();  
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

