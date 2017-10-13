/**********************************************
创建日期：2017/3/22 星期三 16:55:05
作者：张海城
说明:
**********************************************/
using UnityEngine;
using System.Collections;
using System;

public class LevelBorn : LevelElement
{
    public EBattleCamp Camp = EBattleCamp.A;
    private GameObject mBody;

    public override void Build()
    {
        if (mBody == null)
        {
            mBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mBody.transform.parent = transform;
            mBody.transform.localPosition = Vector3.zero;
            mBody.transform.localEulerAngles = Vector3.zero;
            mBody.transform.localScale = Vector3.one;
        }
        MeshRenderer render = mBody.GetComponent<MeshRenderer>();
        if (render == null)
        {
            return;
        }
        if (render.sharedMaterial != null)
        {
            Shader shader = Shader.Find("Custom/TranspUnlit");
            render.sharedMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
        }
        switch (Camp)
        {
            case EBattleCamp.A:
                render.sharedMaterial.color = Color.green;
                break;
            case EBattleCamp.B:
                render.sharedMaterial.color = Color.blue;
                break;
            case EBattleCamp.C:
                render.sharedMaterial.color = Color.yellow;
                break;
        }
    }

    public override void SetName()
    {
        gameObject.name = "Born_" + Camp.ToString();
    }

    public override LoadXML Export()
    {
        MapBorn data = new MapBorn();
        data.Camp = Camp;
        data.TransParam = new MapTransform();
        data.TransParam.Position = Position;
        data.TransParam.Scale = Scale;
        data.TransParam.EulerAngles = Euler;
        return data;
    }

    public override void Import(LoadXML pData, bool pBuild)
    {
        MapBorn data = pData as MapBorn;
        Camp = data.Camp;
        Position = data.TransParam.Position;
        Scale = data.TransParam.Scale;
        Euler = data.TransParam.EulerAngles;
        this.Build();
        this.SetName();
    }
}
