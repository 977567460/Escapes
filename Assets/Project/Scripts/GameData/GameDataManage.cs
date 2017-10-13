using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameDataManage :Singleton<GameDataManage> {

    public Dictionary<int, SceneData> DictScene;
    public override void Init()
    {
        base.Init();
        DictScene=new Dictionary<int, SceneData>();
        new ReadSceneData().Load(DictScene);
       

    }
    public SceneData GetDBScene(int id)
    {
        SceneData db = null;
       
        DictScene.TryGetValue(id, out db);
        return db;
    }

}
