using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameDataManage : Singleton<GameDataManage>
{

    public Dictionary<int, SceneData> DictScene;
    public Dictionary<int, DBEntiny> DictDBEntiny;
    public Dictionary<int, LevelItem> LevelDatas;
    private int mCurRoleID = 1;
    private LevelConfig Config;
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
        LevelDatas = new Dictionary<int, LevelItem>();
        InitXmlScene();

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
    public LevelItem GetLevelItemData(int id)
    {
        LevelItem db = null;
        LevelDatas.TryGetValue(id, out db);
        return db;
    }

    void InitXmlScene()
    {
        string fsPath = ("Text/Role/role");
        Config = new LevelConfig();
        Config.Load(fsPath);
        LevelDatas = Config.LevelItemDic;
    }
}
