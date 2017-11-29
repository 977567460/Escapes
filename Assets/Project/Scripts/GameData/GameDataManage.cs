using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameDataManage : Singleton<GameDataManage>
{

    public Dictionary<int, SceneData> DictScene;
    public Dictionary<int, DBEntiny> DictDBEntiny;
    private int mCurRoleID = 1;
    public int CurRoleID
    {
        get { return mCurRoleID; }
        set { mCurRoleID = value; }
    }
    public override void Init()
    {
        base.Init();
        DictScene = new Dictionary<int, SceneData>();
        new ReadSceneData().Load(DictScene);
        DictDBEntiny = new Dictionary<int, DBEntiny>();
        new ReadDBEntiny().Load(DictDBEntiny);

    }
    public SceneData GetDBScene(int id)
    {
        SceneData db = null;

        DictScene.TryGetValue(id, out db);
        return db;
    }
    public DBEntiny GetDBEntiny(int id)
    {
        DBEntiny db = null;

        DictDBEntiny.TryGetValue(id, out db);
        return db;
    }
}
