using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelEditor : Editor {
    private static LevelEditor mInstance;

    [MenuItem("编辑器/地图编辑器", false)]
    static void OpenSceneEditor()
    {
        LevelManage.Instance.Init();
    }
    void OnDestroy()
    {
        mInstance = null;
    }

}
