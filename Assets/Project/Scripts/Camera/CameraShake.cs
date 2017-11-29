using UnityEngine;
using System.Collections;
using DG.Tweening.Core;
using DG.Tweening;

public class CameraShake : CameraEffectBase
{
    public CameraShake()
    {
        mType = ECameraType.SHAKE;
    }

    public override void Init(int id, Camera cam, CameraEvent callback, params object[] args)
    {
        base.Init(id, cam, callback, args);
    }

    public override void OnEnter()
    {
        this.mCamera.DOShakePosition(1, 2);
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
