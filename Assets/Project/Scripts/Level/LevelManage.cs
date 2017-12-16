using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cfg.Map;
public enum EMapHolder
{
    Born,
    MonsterGroup,
    Role,
}
public class LevelManage : MonoSingleton<LevelManage>
{
    public int MapID;
    public MapConfig Config;
    private static int GUIDStart = 100001;
    private Dictionary<EMapHolder, LevelElement> mHolders = new Dictionary<EMapHolder, LevelElement>();
    public ActorMainPlayer pMainActor = null;

    public int GetGUID()
    {
        GUIDStart++;
        return GUIDStart;
    }
    public void Init()
    {

        AddHolder<HolderBorn>(EMapHolder.Born);
        AddHolder<HolderRole>(EMapHolder.Role);
        AddHolder<MonsterGroup>(EMapHolder.MonsterGroup);
        foreach (KeyValuePair<EMapHolder, LevelElement> current in mHolders)
        {
            Transform trans = current.Value.transform;
            trans.parent = transform;
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
        }

    }
    public void EnterWorld(int mapID)
    {
        this.MapID = mapID;
        this.Init();
        string fsPath = GTTools.Format("Text/Map/{0}", MapID);
        Config = new Cfg.Map.MapConfig();
        Config.Load(fsPath);
        this.OnSceneStart();
    }

    public void OnSceneStart()
    {

        ESceneType pType = StartGame.Instance.GetCurrSceneType();
        int id = GameDataManage.Instance.CurRoleID;
        if (Config.A == null)
        {
            return;
        }
        for (int i = 0; i < Config.Players.Count; i++)
        {
           MapPlayer data = Config.Players[i];          
           ActorMainPlayer actorMainPlayer= AddMainPlayer(data.Id, XTransform.Create(data.Position, data.Euler));
           LevelData.MainPlayerlist.Add(actorMainPlayer);                      
        }
        for (int i = 0; i < Config.Monsters.Count; i++)
        {
            MapMonster data = Config.Monsters[i];
            AddActor(data.Id, EActorType.MONSTER, EBattleCamp.B, data.Position, data.Euler, data.Scale,data.PatrolGroups);
        }

        LevelManage.Instance.SetMainPlayer(1);       
    }

    public ActorMainPlayer AddMainPlayer(int id, XTransform param)
    {      
        pMainActor = (ActorMainPlayer)AddActor(id, EActorType.PLAYER, EBattleCamp.A, param, null, true);
        LevelData.MainPlayer = pMainActor;
        this.SetFollowCamera(LevelData.MainPlayer.Obj);
        return pMainActor;
    }
    public void SetMainPlayer(int id)
    {
        for (int i = 0; i < LevelData.MainPlayerlist.Count; i++)
        {
            if (LevelData.MainPlayerlist[i].Id == id)
            {
                LevelData.MainPlayer = LevelData.MainPlayerlist[i];
                LevelData.MainPlayer.addMainPlayer();
            }
            else
            {
                LevelData.MainPlayerlist[i].RemoveMainPlayer();
            }
        }
        this.SetFollowCamera(LevelData.MainPlayer.Obj);
    }
    public Actor AddActor(int id, EActorType type, EBattleCamp camp, XTransform param, List<Vector3> PatrolGroups,bool isMainPlayer = false)
    {
        int guid = GetGUID();
        Actor pActor = null;
        switch (type)
        {
            case EActorType.PLAYER:
                {
                    if (isMainPlayer)
                    {
                        pActor = new ActorMainPlayer(id, guid, EActorType.PLAYER, camp,null);
                    }
                    else
                    {
                        pActor = new ActorPlayer(id, guid, EActorType.PLAYER, camp,null);
                    }
                }
                break;
            case EActorType.MONSTER:

                pActor = new Actor(id, guid, type, camp, PatrolGroups);
                break;

        }
        if (pActor != null)
        {
            param.Position = GTTools.NavSamplePosition(param.Position);
            pActor.Load(param);
            if (pActor.CacheTransform != null)
            {
                pActor.CacheTransform.parent = GetHolder(EMapHolder.Role).transform;
                LevelData.AllActors.Add(pActor);
                LevelData.CampActors[camp].Add(pActor);
            }
        }
        return pActor;
    }
    public void AddHolder<T>(EMapHolder type) where T : LevelElement
    {
        LevelElement holder = null;
        mHolders.TryGetValue(type, out holder);
        if (holder == null)
        {
            holder = new GameObject(typeof(T).Name).AddComponent<T>();
            mHolders[type] = holder;
        }
    }
    public LevelElement GetHolder(EMapHolder type)
    {
        LevelElement holder = null;
        mHolders.TryGetValue(type, out holder);
        return holder;
    }
    public Actor AddActor(int id, EActorType type, EBattleCamp camp, Vector3 pos, Vector3 angle, List<Vector3> PatrolGroups)
    {
        return AddActor(id, type, camp, XTransform.Create(pos, angle), PatrolGroups);
    }

    public Actor AddActor(int id, EActorType type, EBattleCamp camp, Vector3 pos, Vector3 angle, Vector3 scale, List<Vector3> PatrolGroups)
    {
        return AddActor(id, type, camp, XTransform.Create(pos, angle, scale), PatrolGroups);
    }
    public Camera SetFollowCamera(GameObject player)
    {
        Camera cam = CameraManage.Instance.MainCamera;
        object[] args = new object[] { player.transform };
        CameraManage.Instance.SwitchCameraEffect(ECameraType.FOLLOW, cam, null, args);
        return cam;
    }
}
public class XTransform
{
    public Vector3 Position = Vector3.zero;
    public Vector3 EulerAngles = Vector3.zero;
    public Vector3 Scale = Vector3.one;

    public static XTransform Create(Vector3 pos, Vector3 angle)
    {
        XTransform data = new XTransform();
        data.Position = pos;
        data.EulerAngles = angle;
        data.Scale = Vector3.one;
        return data;
    }

    public static XTransform Create(Vector3 pos, Vector3 angle, Vector3 scale)
    {
        XTransform data = new XTransform();
        data.Position = pos;
        data.EulerAngles = angle;
        data.Scale = scale;
        return data;
    }

    static XTransform mDefault = new XTransform();
    public static XTransform Default { get { return mDefault; } }
}