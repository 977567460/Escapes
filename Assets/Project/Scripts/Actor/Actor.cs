/**********************************************
创建日期：2017/11/24 星期五 10:56:50
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class Actor : ICharacter
{
    protected ActorAttr mCurrAttr = new ActorAttr();
    protected List<Actor> mEnemys = new List<Actor>();
    protected IStateMachine<Actor, FSMState> mMachine;
    public ZTAction mActorAction;
    protected Animator mActorAnimator;
    protected ActorBehavior mBehavior;
    protected XTransform mBornParam;
    protected CharacterController mCharacter;
    protected Actor mTarget;       //当前目标
    protected ActorAI mActorAI;
    public ActorPathFinding mActorPathFinding;
    public List<Vector3> PatrolGroups;
    private Actor murderer;//凶手
    public EActorType ActorType { get; private set; }
    public EBattleCamp Camp { get; set; }
    public EMonsterType MonsterType { get; private set; }
    public ActorPart mActorPart{ get; private set; }
    public AIConeDetection AiConeDetection;
    public Actor(int id, int guid, EActorType type, EBattleCamp camp, List<Vector3> PatrolGroups)
        : base(id, guid)
    {
        this.ActorType = type;
        this.Camp = camp;
        this.PatrolGroups = PatrolGroups;
        this.MonsterType = GameDataManage.Instance.GetDBEntiny(Id).MonsterType;       
    }
    public void ApplyAnimator(bool enabled)
    {
        if (mActorAnimator != null)
        {
            mActorAnimator.enabled = enabled;
        }
    }
    public FSMState FSM
    {
        get
        {
            if (this.mMachine == null)
            {
                return FSMState.FSM_BORN;
            }
            return (FSMState)this.mMachine.GetCurrStateID();
        }
    }

    public override void Init()
    {
        InitAttr();
        mActorPathFinding = new ActorPathFinding(this);
        InitAction();
        InitFSM();        
        InitBehavior();
        
        InitAI();

        this.AddCommands();        
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
        this.mMachine.AddState(FSMState.FSM_Attack, new ActorAttackFsm());
        this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_IDLE));
        this.mMachine.GetState(this.mMachine.GetCurrStateID()).Enter();
    }
    protected void InitAI()
    {
        mActorAI = new ActorAI(this);
        mActorAI.Start();
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
    public void InitAttr(bool init = false)
    {
        Dictionary<EProperty, int> propertys = null;
        DBEntiny db = GameDataManage.Instance.GetDBEntiny(Id);            
        propertys = db.Propertys;
        mCurrAttr.CopyFrom(propertys);
        mCurrAttr.Update(EAttr.Speed, (int)db.RSpeed);
        mCurrAttr.Update(EAttr.StartAngle, (int)db.StartAngle);
        mCurrAttr.Update(EAttr.EndAngle, (int)db.EndAngle);
        mCurrAttr.Update(EAttr.ViewLength, (int)db.ViewLength);
        mCurrAttr.Update(EAttr.WaitPatrolTime, (int)db.WaitPatrolTime);      
       
    }
    public float Height
    {
        get { return mCharacter.height * CacheTransform.localScale.x; }
    }

    public float Radius
    {
        get { return mCharacter.radius * CacheTransform.localScale.x; }
    }
    private void AddCommands()
    {    
        this.Receiver.AddCommand<RTCommand>(ECommand.TYPE_RUNTO, CheckRunTo);
        
    }
    protected virtual ECommandReply CheckRunTo(RTCommand cmd)
    {   
        this.GetActorAI().AIMode = EAIMode.Auto;
        SendStateMessage(FSMState.FSM_RUN, cmd);
        return ECommandReply.Y; ;
    }
    public void ApplyRootMotion(bool enabled)
    {
        if (mActorAnimator != null)
        {
            mActorAnimator.applyRootMotion = enabled;
        }
    }
    public virtual void OnPursue(RTCommand ev)
    {
        this.mActorPathFinding.SetOnFinished(ev.Callback);
        MoveTo(ev.DestPosition);
        this.mActorAction.Play("Run", null, true);
    }
    public virtual void OnWalk()
    {
        this.mActorAction.Play("Walk", null, true);
    }
    public virtual void OnIdle()
    {        
        StopPathFinding();
        this.mActorAction.Play("Idle", null, true);
    }
    public virtual void OnRun()
    {
        this.mActorAction.Play("Run", null, true);
    }
    public virtual void OnDead()
    {
        StopPathFinding();
        this.mActorAction.Play("Dead", null, false);
        this.Clear();
        this.ApplyCharacterCtrl(false);
        this.mActorAI.Clear();
    }
    public virtual void OnJump()
    {
        StopPathFinding();
        this.mActorAction.Play("Jump", GotoEmptyFSM,false);
    }
    public virtual void OnAttack()
    {     
        if(this.ActorType==EActorType.MONSTER){
            StopPathFinding();
            CacheTransform.LookAt(this.mTarget.CacheTransform);
            this.mActorAction.Play("Gun", GotoEmptyFSM, true);
        }
        else
        {
            this.mActorAction.Play("Knife2", GotoEmptyFSM, false);
        }
            
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
        this.mActorPart=new ActorPart(this);        
        this.mCharacter = Obj.GetComponent<CharacterController>();
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
    public void ChangeState(FSMState fsm, ICommand ev)
    {
        if (mMachine == null || CacheTransform == null)
        {
            return;
        }
        if (FSM == FSMState.FSM_DEAD && fsm != FSMState.FSM_REBORN)
        {
            return;
        }
        if (!mMachine.Contains(fsm))
        {
            return;
        }
        mMachine.GetState(fsm).SetCommand(ev);
        mMachine.ChangeState(fsm);
    }
    public void GotoEmptyFSM()
    {
        ChangeState(FSMState.FSM_EMPTY, null);
    }
    public void SendStateMessage(FSMState fsm)
    {
        ChangeState(fsm, null);
    }

    public void SendStateMessage(FSMState fsm, ICommand ev)
    {
        ChangeState(fsm, ev);
    }
    public override void Destroy()
    {
        mActorAction.Clear();
        mMachine.Clear();
        mActorAI.Clear();
    }

    public override void Clear()
    {
        
    }

    public override void Step()
    {
        if (CacheTransform == null || mMachine == null)
        {
            return;
        }

        mMachine.Step();
        mActorPathFinding.Step();
        mActorAI.Step();
    }

    public override void ChangeModel(int modelID)
    {
        
    }
    public virtual bool CannotControlSelf()
    {
        switch (FSM)
        {
            case FSMState.FSM_Attack:
            case FSMState.FSM_DEAD:
            case FSMState.FSM_JUMP:
                return true;
            default:
                return false;
        }
    }
    public override bool IsDead()
    {
        return FSM == FSMState.FSM_DEAD;
    }

    public override bool IsDestroy()
    {
        return CacheTransform == null;
    }

    public override void Pause(bool pause)
    {
        this.ApplyAnimator(!pause);
        if (!pause)
        {
            SendStateMessage(FSMState.FSM_IDLE);
        }
    }
    public void ApplyCharacterCtrl(bool enabled)
    {
        if (mCharacter != null)
        {
            mCharacter.enabled = enabled;
        }
    }
    public virtual void OnForceToMove(MVCommand ev)
    {
        StopPathFinding();
        Vector2 delta = ev.Delta;
        CacheTransform.LookAt(new Vector3(CacheTransform.position.x + delta.x, CacheTransform.position.y, CacheTransform.position.z + delta.y));
        mCharacter.SimpleMove(mCharacter.transform.forward*mCurrAttr.Speed);
        this.mActorAction.Play("Walk", null, true);
    }
    public void UpdateAttr(EAttr attr, int value)
    {      
        mCurrAttr.Update(attr, value);
        ZTEvent.FireEvent(EventID.REQ_PLAYER_Attr);
    }
    public void BeDamage(Actor attacker, int damage, bool critial = false)
    {
        if (this.mCurrAttr.HP > damage)
        {
            UpdateAttr(EAttr.HP, this.mCurrAttr.HP - damage);
        }
        else
        {
            UpdateAttr(EAttr.HP, 0);
        }
        if (this.mCurrAttr.HP <= 0)
        {
            if (ActorType == EActorType.PLAYER)
            {
                murderer = attacker;
                Talk("55555555555555");
            }

            SendStateMessage(FSMState.FSM_DEAD);
        }
    }
    public void BeDamage(int damage)
    {
        if (this.mCurrAttr.HP > damage)
        {
            UpdateAttr(EAttr.HP, this.mCurrAttr.HP - damage);
        }
        else
        {
            UpdateAttr(EAttr.HP, 0);
        }
        if (this.mCurrAttr.HP <= 0)
        {
            if (FSM != FSMState.FSM_DEAD)
            SendStateMessage(FSMState.FSM_DEAD);
        }
    }
    public ActorAttr GetCurrAttr()
    {
        return mCurrAttr;
    }
    public int GetAttr(EAttr attr)
    {
        return this.mCurrAttr.GetAttr(attr);
    }
    public XTransform GetBornParam()
    {
        return mBornParam;
    }
    public Actor GetTarget()
    {
        return mTarget;
    }
    public Vector3 Pos
    {
        get { return CacheTransform.position; }
    }
    public ActorAI GetActorAI()
    {
        return mActorAI;
    }
    public ETargetCamp GetTargetCamp(Actor actor)
    {
        if (actor.Camp == EBattleCamp.D)
        {
            return ETargetCamp.None;
        }
        if (actor.Camp == Camp)
        {
            return ETargetCamp.Ally;
        }
        if (actor.Camp != EBattleCamp.C && Camp != EBattleCamp.C)
        {
            return ETargetCamp.Enemy;
        }
        return ETargetCamp.Neutral;
    }
    public void FindActorsByTargetCamp(ETargetCamp actorCamp, ref List<Actor> list, bool ignoreStealth = false)
    {
        for (int i = 0; i < LevelData.AllActors.Count; i++)
        {
            Actor actor = LevelData.AllActors[i];           
            if (GetTargetCamp(actor) == actorCamp && actor.IsDead() == false)
            {
                if (ignoreStealth == false)
                {
                    list.Add(actor);
                }
                else
                {                   
                    list.Add(actor);
                    
                }
            }
        }
    }
    public List<Actor> GetAllEnemy()
    {
        mEnemys.Clear();
        FindActorsByTargetCamp(ETargetCamp.Enemy, ref mEnemys, true);
        return mEnemys;
    }
    public Actor GetNearestEnemy(float radius = 10000)
    {
        if (AiConeDetection.TargetPlayer != null)
        {
            return AiConeDetection.TargetPlayer;
        }
        List<Actor> actors = GetAllEnemy();       
        Actor nearest = null;
        float min = radius;
        for (int i = 0; i < actors.Count; i++)
        {
            float dist = GTTools.GetHorizontalDistance(actors[i].CacheTransform.position, this.CacheTransform.position);
            if (dist < min)
            {
                min = dist;
                nearest = actors[i];
            }
        }
        return nearest;
    }
    public void SetTarget(Actor actor)
    {
        if (actor == null)
        {
            this.mTarget = null;
            return;
        }
        if (mTarget == actor)
        {
            return;
        }
        this.mTarget = actor;
      //  CacheTransform.LookAt(this.mTarget.CacheTransform);
    } 
    public void OnArrive()
    {
        GotoEmptyFSM();      
    }
  
    public virtual void MoveTo(Vector3 destPosition)
    {
        mActorPathFinding.SetDestPosition(destPosition);
    }

    public virtual void StopPathFinding()
    {
        mActorPathFinding.StopPathFinding();
    }
    public ActorBehavior Behavior
    {
        get { return mBehavior; }
    }
    public  void TalkOther()
    {
        List<Actor> Moster = LevelData.GetActorsByActorType(EActorType.MONSTER);
        for (int i = 0; i < Moster.Count; i++)
        {
            if (Moster[i].GetTarget() == null) return;
            Moster[i].GetActorAI().ChangeAIState(EAIState.AI_CHASE);
        }
    }
    public void Talk(string talkvalue)
    {      
        GameObject talk = ZTPool.Instance.GetGo("UI/Game/Talk");
        Transform _Canvas = GameObject.Find("Canvas").transform;
        talk.transform.SetParent(_Canvas);
        talk.transform.localPosition = Vector3.zero;
        talk.transform.localEulerAngles = Vector3.zero;
        TalkSet talkSet = talk.GET<TalkSet>();
        talkSet.murderer = murderer;
        talkSet.TalkText = talkSet.transform.GetComponent<Text>();
        talkSet.SetText(talkvalue);

    }
}

