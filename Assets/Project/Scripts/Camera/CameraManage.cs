using UnityEngine;
using System.Collections;

public class CameraManage : MonoSingleton<CameraManage>
{
    private Transform _Canvas = null;
    public Camera MainCamera { get; set; }

    public override void SetDontDestroyOnLoad(Transform parent)
    {
        base.SetDontDestroyOnLoad(parent);
        // this.CreateMainCamera();
        _Canvas = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(_Canvas);
        CreateMainCamera();
        GameObject.DontDestroyOnLoad(MainCamera);
    }
    public Camera CreateCamera(string name)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        return go.AddComponent<Camera>();
    }
    public void CreateMainCamera()
    {
        MainCamera = Camera.main;
        if (MainCamera == null)
        {
            GameObject c = new GameObject("MainCamera");
            MainCamera = c.AddComponent<Camera>();
            c.tag = "MainCamera";
            MainCamera.gameObject.AddComponent<AudioListener>();
        }
        MainCamera.transform.parent = transform;
    }

    public void SwitchCamera(Camera cam)
    {
        cam.gameObject.AddComponent<CameraFollow>();
    }
    public void RevertMainCamera()
    {
        if (MainCamera == null)
        {
            return;
        }
        if (MainCamera.GetComponent<CameraFollow>() != null)
        {
            Destroy(MainCamera.GetComponent<CameraFollow>());
        }
        MainCamera.fieldOfView = 60;
        MainCamera.renderingPath = RenderingPath.Forward;
        MainCamera.depth = Define.DEPTH_CAM_MAIN;
    }
    public void AddUI(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        go.transform.SetParent(_Canvas);
        // go.transform.localPosition = Vector3.zero;
        //  go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }
}
