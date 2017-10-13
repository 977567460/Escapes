/**********************************************
创建日期：2017/3/22 星期三 16:13:17
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;

public class LevelBarrier : LevelElement
{
    public float Width = 14;
    private Transform mBody;
    private BoxCollider mCollider;
    private Vector3 mSize;

    public override void Build()
    {
        if (Width < Define.BARRIER_WIDTH)
        {
            Width = Define.BARRIER_WIDTH;
        }
        int count = Mathf.CeilToInt(Width / Define.BARRIER_WIDTH);
        mSize.x = count * Define.BARRIER_WIDTH;
        mSize.y = 4;
        mSize.z = 1.5f;

        mBody = transform.Find("Body");
        if (mBody == null)
        {
            mBody = new GameObject("Body").transform;
            mBody.parent = transform;
            mBody.transform.localPosition = Vector3.zero;
            mBody.localEulerAngles = Vector3.zero;
        }
        else
        {
            GameObject.Destroy(mBody);
        }
        float halfCount = count * 0.5f;
        for (int i = 0; i < count; i++)
        {
            GameObject unit = LoadResource.Instance.Instantiate(Define.BARRIER_PREFAB);
            if (unit == null)
            {
                return;
            }
            unit.name = i.ToString();
            Transform trans = unit.transform;
            Vector3 localPosition = Vector3.right * (i - halfCount + 0.5f) * Define.BARRIER_WIDTH;
            localPosition.z = mSize.z * 0.5f;
            trans.localPosition = localPosition;
            trans.SetParent(mBody, false);
        }
        mCollider = gameObject.GetComponent<BoxCollider>();
        mCollider.size = mSize;
        mCollider.center = new Vector3(0, mSize.y * 0.5f, mSize.z * 0.5f);
        gameObject.layer= Define.LAYER_BARRER;

    }

    public override void SetName()
    {
        gameObject.name = "Barrier_" + Id.ToString();
    }

    public override LoadXML Export()
    {
        MapBarrier data = new MapBarrier();
        data.Id = Id;
        data.Width = Width;
        data.TransParam = new MapTransform();
        data.TransParam.Position = Position;
        data.TransParam.Scale = Scale;
        data.TransParam.EulerAngles = Euler;
        return data;
    }

    public override void Import(LoadXML pData, bool pBuild)
    {
        MapBarrier data = pData as MapBarrier;
        Id = data.Id;
        Width = data.Width;
        Position = data.TransParam.Position;
        Scale = data.TransParam.Scale;
        Euler = data.TransParam.EulerAngles;
        this.Build();
        this.SetName();
    }
}
