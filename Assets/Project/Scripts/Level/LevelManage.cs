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

    public int GetGUID()
    {
        GUIDStart++;
        return GUIDStart;
    }
    public void Init()
    {

        AddHolder<HolderBorn>(EMapHolder.Born);
        AddHolder<HolderRole>(EMapHolder.Role);
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
        AddMainPlayer(id, XTransform.Create(Config.A.TransParam.Position, Config.A.TransParam.EulerAngles));


    }

    public ActorMainPlayer AddMainPlayer(int id, XTransform param)
    {
        ActorMainPlayer pActor = (ActorMainPlayer)AddActor(id, EActorType.PLAYER, EBattleCamp.A, param, true);
        LevelData.MainPlayer = pActor;
        this.SetFollowCamera(LevelData.MainPlayer.Obj);
        return pActor;
    }
    public Actor AddActor(int id, EActorType type, EBattleCamp camp, XTransform param, bool isMainPlayer = false)
    {
        int guid = GetGUID();
        Actor pActor = null;
        switch (type)
        {
            case EActorType.PLAYER:
                {
                    if (isMainPlayer)
                    {
                        pActor = new ActorMainPlayer(id, guid, EActorType.PLAYER, camp);
                    }
                    else
                    {
                        pActor = new ActorPlayer(id, guid, EActorType.PLAYER, camp);
                    }
                }
                break;
            case EActorType.MONSTER:

                pActor = new Actor(id, guid, type, camp);
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
    public Actor AddActor(int id, EActorType type, EBattleCamp camp, Vector3 pos, Vector3 angle)
    {
        return AddActor(id, type, camp, XTransform.Create(pos, angle));
    }

    public Actor AddActor(int id, EActorType type, EBattleCamp camp, Vector3 pos, Vector3 angle, Vector3 scale)
    {
        return AddActor(id, type, camp, XTransform.Create(pos, angle, scale));
    }
    public Camera SetFollowCamera(GameObject player)
    {
        Camera cam = CameraManage.Instance.MainCamera;
        object[] args = new object[] { player.transform };
        CameraManage.Instance.SwitchCamera(cam);
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