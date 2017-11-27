using UnityEngine;
using System.Collections;

public class LevelManage : Singleton<LevelManage>
{
    public int MapID;
    public float Delay;
    public string MapName = string.Empty;
    public string MapPath = string.Empty;
    public override void Init()
    {
        base.Init();

    }
    public void EnterWorld(int mapID)
    {
        //this.MapID = mapID;
        //this.Init();
        //string fsPath = GTTools.Format("Text/Map/{0}", MapID);
        //Config = new Cfg.Map.MapConfig();
        //Config.Load(fsPath);
        //for (int i = 0; i < Config.Regions.Count; i++)
        //{
        //    MapRegion data = Config.Regions[i];
        //    if (data.StartActive)
        //    {
        //        LevelElement pHolder = GetHolder(EMapHolder.Region);
        //        GameObject go = NGUITools.AddChild(pHolder.gameObject);
        //        LevelRegion pRegion = go.AddComponent<LevelRegion>();
        //        pRegion.Import(data, false);
        //        pRegion.Init();
        //    }
        //}
        //this.OnSceneStart();
    }
}
