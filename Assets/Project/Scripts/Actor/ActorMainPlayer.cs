using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ActorMainPlayer : ActorPlayer
{
    private float JumpCD;
    private float AttackCD;
    private float Timmer=0;
    private bool IsStone = false;
    private BottleScript bottlescript;
    private GameObject Bottle;
    public ActorMainPlayer(int id, int guid, EActorType type, EBattleCamp camp, List<Vector3> PatrolGroups)
        : base(id, guid, type, camp, PatrolGroups)
    {

    }
    public override void Init()
    {
        base.Init();
        JumpCD = mActorAction.GetAnimLength("Jump");
        AttackCD = 4;
    }
    void OnMainPlayerJump()
    {
        if (Timmer > 0) return;
        this.SendStateMessage(FSMState.FSM_JUMP);
        Timmer = JumpCD;
      
    }
    void OnMainPlayerAttack()
    {
        if(Timmer>0) return;
        this.SendStateMessage(FSMState.FSM_Attack);

        ZTCoroutinue.Instance.StartCoroutine(AtkCondition1(3, 30));
        Timmer = AttackCD;
    }
    void OnMainPlayerWalk(float arg1, float arg2)
    {
        if (CannotControlSelf()) return;
        Vector2 delta = new Vector2(arg1, arg2);
        this.SendStateMessage(FSMState.FSM_WALK, new MVCommand(delta));
    }
    void OnMainPlayerIdle()
    {
        if (CannotControlSelf()) return;
        this.SendStateMessage(FSMState.FSM_IDLE);
    }

    void SetMainPlayer(int id)
    {    
        LevelManage.Instance.SetMainPlayer(id);            
        ZTEvent.FireEvent(EventID.REQ_PLAYER_Attr);
    }
    void TakeStone()
    {
      
        if (IsStone)
        {
            bottlescript.isDrag = false;
            IsStone = false;
            AiConeDetection.GroundMesh.SetActive(false);
            if (Bottle != null)
            {
                ZTPool.Instance.ReleaseGo("Model/Weapons/Bottle", Bottle,EPoolObjectType.PEntity);
            }
        

        }
        else
        {
            Bottle = ZTPool.Instance.GetGo("Model/Weapons/Bottle");
            bottlescript = Bottle.GET<BottleScript>();
            bottlescript.isDrag = true;
            bottlescript.actor = this;
            IsStone = true;
            AiConeDetection.GroundMesh.SetActive(true);
        }
   
    }
    void ThrowingStone()
    {
        if (bottlescript != null && IsStone)
        {
            bottlescript.isDrag = false;
            IsStone = false;
            AiConeDetection.GroundMesh.SetActive(false);
            for (int i = 0; i < GetAllEnemy().Count; i++)
            {
                if (Vector3.Distance(GetAllEnemy()[i].CacheTransform.position, bottlescript.gameObject.transform.position)<=10)
                GetAllEnemy()[i].mActorPathFinding.SetDestPosition(bottlescript.gameObject.transform.position);
            }
        }
       
    }
    public void addMainPlayer()
    {
  
        ZTEvent.AddHandler(EventID.REQ_PLAYER_JUMP, OnMainPlayerJump);
        ZTEvent.AddHandler<float, float>(EventID.REQ_PLAYER_Walk, OnMainPlayerWalk);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_Idle, OnMainPlayerIdle);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_Attack, OnMainPlayerAttack);
        ZTEvent.AddHandler<int>(EventID.REQ_PLAYER_Change, SetMainPlayer);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_TakeStone, TakeStone);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_ThrowingStone, ThrowingStone);
        ZTEvent.AddHandler<Actor>(EventID.REQ_PLAYER_EnemyArea, EnemyArea);
    }
    public void RemoveMainPlayer()
    {
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_JUMP, OnMainPlayerJump);
        ZTEvent.RemoveHandler<float, float>(EventID.REQ_PLAYER_Walk, OnMainPlayerWalk);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_Idle, OnMainPlayerIdle);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_Attack, OnMainPlayerAttack);
        ZTEvent.RemoveHandler<int>(EventID.REQ_PLAYER_Change, SetMainPlayer);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_TakeStone, TakeStone);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_ThrowingStone, ThrowingStone);
        ZTEvent.RemoveHandler<Actor>(EventID.REQ_PLAYER_EnemyArea, EnemyArea);
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
        AudioClip clip = LoadResource.Instance.Load<AudioClip>("Sounds/YiJianMei");
        ZTAudio.Instance.PlayMusic(clip);
        
    }

    public override void Step()
    {
        base.Step();
        if (Timmer > 0)
        {
            Timmer -= Time.deltaTime;
        }
        else
        {
            Timmer = 0;
        }
      
    }
    IEnumerator AtkCondition1(float _range, float _angle)
    {
        yield return new WaitForSeconds(1.5f);
        AudioClip clip = LoadResource.Instance.Load<AudioClip>("Sounds/SWORD05");
        ZTAudio.Instance.PlaySound(clip);
        yield return new WaitForSeconds(0.5f);
       
        // 搜索所有敌人列表（在动态创建敌人时生成的）
        // 列表存储的并非敌人的GameObject而是自定义的Enemy类
        // Enemy类的一个变量mGameObject则用来存储实例出来的敌人实例
        foreach (Actor go in LevelData.GetActorsByActorType(EActorType.MONSTER))
        {
            // 敌人的坐标向量减去Player的坐标向量的长度（使用magnitude）
            float tempDis1 = Vector3.Distance(go.Obj.transform.position, this.CacheTransform.position);
            // 敌人向量减去Player向量就能得到Player指向敌人的一个向量
            Vector3 v3 = go.Obj.transform.position - this.CacheTransform.position;
            // 求出Player指向敌人和Player指向正前方两向量的夹角，其实就是Player和敌人的夹角(不分左右)
            float angle = Vector3.Angle(v3, this.CacheTransform.forward);
            // Debug.Log("tempDis1:" + tempDis1 + "_range:" + _range + "angle:" + angle + "_angle:" + _angle);
            if (tempDis1 < _range && angle < _angle)
            {
                Debug.Log("damage");
                // 距离和角度条件都满足了
                go.BeDamage(this.GetAttr(EAttr.Atk));
            }
        }
    }
    private void DragCondition1(float _range)
    {
        // 搜索所有敌人列表（在动态创建敌人时生成的）
        // 列表存储的并非敌人的GameObject而是自定义的Enemy类
        // Enemy类的一个变量mGameObject则用来存储实例出来的敌人实例
        foreach (Actor go in LevelData.GetActorsByActorType(EActorType.MONSTER))
        {
            // 敌人的坐标向量减去Player的坐标向量的长度（使用magnitude）
            float tempDis1 = Vector3.Distance(go.Obj.transform.position, this.CacheTransform.position);
            if (tempDis1 < _range)
            {
                if (go.IsDead())
                    go.CacheTransform.position = this.CacheTransform.position;

            }
        }
    }
    void EnemyArea(Actor actor)
    {
        for (int i = 0; i < GetAllEnemy().Count; i++)
        {
            GetAllEnemy()[i].AiConeDetection.GroundMesh.gameObject.SetActive(false);
        }
        actor.AiConeDetection.GroundMesh .gameObject.SetActive(true);
    }
}

