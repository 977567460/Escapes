using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow :CameraEffectBase
{
    public CameraFollow()
    {
        mType = ECameraType.FOLLOW;
    }

    public float distance=-12;
    public float height = 30;
    public float angle = 90;
    public Transform Follow;
    private GameObject  lastobj;
    public override void OnUpdate()
    {
        if (Follow == null)
        {
            return;
        }
        Vector3 pos = Follow.position + new Vector3(0, height, distance);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5);
        transform.LookAt(Follow);
      //  SetObjDisable(Follow);
    }

    public override void Init(int id, Camera cam, CameraEvent callback, params object[] args)
    {
        base.Init(id, cam, callback, args);
        Follow = (Transform)args[0];
        Vector3 pos = Follow.position + Vector3.up * height + Follow.forward * distance;
        transform.position = pos;
        transform.LookAt(Follow); 
    }
    //上次碰撞到的物体
    private List<GameObject> lastColliderObject=new List<GameObject>();
    //本次碰撞到的物体
    private List<GameObject> colliderObject=new List<GameObject>();
    public void SetObjDisable(Transform Target)
    {
        float dis = Vector3.Distance(Target.position ,transform.position);

        Debug.DrawRay(transform.position, transform.forward*50, Color.red);
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward*50);
        
        for (int i = 0; i < colliderObject.Count; i++)
        {
            lastColliderObject.Add(colliderObject[i]);
        }
        colliderObject.Clear();//清空本次碰撞到的所有物体
        for (int i = 0; i < hit.Length; i++) {
            if (hit[i].collider.gameObject.GetComponent<MeshRenderer>() == null) return;
            colliderObject.Add(hit[i].collider.gameObject);
            SetMaterialsColor(hit[i].collider.gameObject.GetComponent<MeshRenderer>(), 0.5f);
        }
        for (int i = 0; i < lastColliderObject.Count; i++)
        {
            for (int ii = 0; ii < colliderObject.Count; ii++)
            {
                if (colliderObject[ii] != null)
                {
                    if (lastColliderObject[i] == colliderObject[ii])
                    {
                        lastColliderObject[i] = null;
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < lastColliderObject.Count; i++)
        {
            if (lastColliderObject[i] != null)
                SetMaterialsColor(lastColliderObject[i].gameObject.GetComponent<MeshRenderer>(),1f);
        }
    }
    /// 置物体所有材质球颜色 <summary>
    /// 置物体所有材质球颜色
    /// </summary>
    /// <param name="_renderer">材质</param>
    /// <param name="Transpa">透明度</param>
    private void SetMaterialsColor(Renderer _renderer, float Transpa)
    {
        //获取当前物体材质球数量
        int materialsNumber = _renderer.sharedMaterials.Length;
        for (int i = 0; i < materialsNumber; i++)
        {
            //获取当前材质球颜色
            Color color = _renderer.materials[i].color;

            //设置透明度  取值范围：0~1;  0 = 完全透明
            color.a = Transpa;

            //置当前材质球颜色
            _renderer.materials[i].SetColor("_Color", color);
        }
    }
}
