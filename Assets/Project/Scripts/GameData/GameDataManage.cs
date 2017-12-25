using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameDataManage : Singleton<GameDataManage>
{

    public Dictionary<int, SceneData> DictScene;
    public Dictionary<int, DBEntiny> DictDBEntiny;
    public List<LevelItem> LevelListDatas;
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
        LevelListDatas = new List<LevelItem>();
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
        for (int i = 0; i < LevelListDatas.Count; i++)
        {
            if (LevelListDatas[i].sceneid == id)
            {
                db = LevelListDatas[i];
                return db;
            }
        }
        return null;
       
    }
    public void SetLevelItemData(int id, int star, bool isopen, float passtime)
    {
        for (int i = 0; i < LevelListDatas.Count; i++)
        {
            if (LevelListDatas[i].sceneid == id)
            {          
                LevelListDatas[i].star = star;
                LevelListDatas[i+1].isopen = isopen;
                LevelListDatas[i].passtime = passtime;
            }
        }

    }
    void InitXmlScene()
    {
        string fsPath = ("Text/Role/role");
        Config = new LevelConfig();
        Config.Load(fsPath);
        LevelListDatas = Config.SceneGroups;
    }
}
