using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : CameraEffectBase
{
    public CameraMove()
    {
        mType = ECameraType.MOVE;
    }

    public override void OnUpdate()
    {
   
    }

    public override void Init(int id, Camera cam, CameraEvent callback, params object[] args)
    {
        base.Init(id, cam, callback, args);                      
        ZTEvent.AddHandler<float, float>(EventID.REQ_Camera_Move,MainCameraMove);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        this.GetComponent<CameraFollow>().enabled = false;
        transform.position = LevelData.MainPlayer.CacheTransform.position+new Vector3(0,30,0);
        transform.localEulerAngles = new Vector3(90,0,0);
 
    }
    public void MainCameraMove(float x, float y)
    {
        transform.position += new Vector3(x, 0, y);
    }
    public override void OnLeave()
    {
        base.OnLeave();
        this.GetComponent<CameraFollow>().enabled = true;
        ZTEvent.RemoveHandler<float, float>(EventID.REQ_Camera_Move, MainCameraMove);
    }
}
