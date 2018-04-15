using UnityEngine;
using System.Collections;
using System.IO;


public class LoadResource : Singleton<LoadResource>
{

    public string GetExtPath()
    {
        string path = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                path = Application.streamingAssetsPath;
                break;
            case RuntimePlatform.Android:
                path = Application.persistentDataPath;
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.persistentDataPath;
                break;
            default:
                path = Application.streamingAssetsPath;
                break;

        }
        return path;
    }
    public string GetDataPath()
    {
        return Application.streamingAssetsPath;
    }
    public T Load<T>(string path, bool instance = false) where T : UnityEngine.Object
    {
        T asset = Resources.Load<T>(path) as T;
        if (asset != null && instance)
        {
            return UnityEngine.Object.Instantiate(asset);
        }
        return asset;
    }
    public GameObject LoadWindow(string resName)
    {
        string path = string.Format("{0}", resName);
        GameObject prefab = Load<GameObject>(path);
        if (prefab == null)
        {
            return null;
          
        }
        GameObject go = GameObject.Instantiate(prefab);
       
        return go;
    }
    public GameObject Instantiate(string path)
    {
        GameObject asset = Load<GameObject>(path);
        GameObject go = null;
        if (asset != null)
        {
            go = GameObject.Instantiate(asset);
        }
        return go;
    }
    public string LoadFromStreamingAssets(string sPath, string pPath)
    {
        string targetPath = Application.persistentDataPath + sPath;
        string sourcePath = Application.streamingAssetsPath + pPath;
        if (Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            WWW www = new WWW(sourcePath);
            bool boo = true;
            while (boo)
            {
                if (www.isDone)
                {
                    File.WriteAllBytes(targetPath, www.bytes);
                    boo = false;
                }
            }
            return targetPath;
        }
        else
        {
            return sourcePath;
        }
    }
}
