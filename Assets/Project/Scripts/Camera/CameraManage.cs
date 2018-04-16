using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public void SwitchCameraEffect(ECameraType type, Camera cam, CameraEvent callback, params object[] args)
    {
        if (cam == null) return;
        List<CameraEffectBase> list = new List<CameraEffectBase>();
        cam.GetComponents(list);
        CameraEffectBase effect = null;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Type == type)
            {
                effect = list[i];
            }
            else
            {
                if (list[i].enabled && list[i].Type != ECameraType.FOLLOW)
                {
                    list[i].SwitchState(ECameraState.LEAVE);
                }
            }
        }
        if (effect == null)
        {
            AddCameraEffect(ref effect, type, cam);
        }
        effect.Init(0, cam, callback, args);      
    }
    public void AddCameraEffect(ref CameraEffectBase effect, ECameraType type, Camera cam)
    {
        switch (type)
        {
            case ECameraType.FOLLOW:
                effect = cam.gameObject.AddComponent<CameraFollow>();
                break;
            case ECameraType.SHAKE:
                effect = cam.gameObject.AddComponent<CameraShake>();
                break;
            case ECameraType.MOVE:
                effect = cam.gameObject.AddComponent<CameraMove>();
                break;
        }
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

    public void SetFollowDis(float Height)
    {
        if (MainCamera.GetComponent<CameraFollow>() != null)
        {
            if (MainCamera.GetComponent<CameraFollow>().height >= 30 && Height>0) return;
            if (MainCamera.GetComponent<CameraFollow>().height <= 10 && Height < 0) return;
            MainCamera.GetComponent<CameraFollow>().height += Height;
        }
    }
   
}
