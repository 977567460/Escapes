using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ControllerType
{
    mPlayer,
    mCamera,
}
public class InputManage : MonoSingleton<InputManage>
{
    public KeyCode jump { get; set; }
    public KeyCode attack { get; set; }
    public KeyCode changemainplayer { get; set; }
    public KeyCode takestone { get; set; }

    public KeyCode switchcontrolier { get; set; }
    RaycastHit hit;
    public ControllerType controllertype;
    void Start()
    {
        jump = KeyCode.Space;
        attack = KeyCode.E;
        changemainplayer=KeyCode.Q;
        takestone = KeyCode.R;
        switchcontrolier = KeyCode.X;
        controllertype = ControllerType.mPlayer;
    
    }
    public override void SetDontDestroyOnLoad(Transform parent)
    {
        base.SetDontDestroyOnLoad(parent);

    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(switchcontrolier))
        {
            Camera cam = CameraManage.Instance.MainCamera;
            object[] args = new object[] { LevelData.MainPlayer.CacheTransform };
            if (controllertype == ControllerType.mPlayer)
            {
                controllertype = ControllerType.mCamera;             
                CameraManage.Instance.SwitchCameraEffect(ECameraType.MOVE, cam, null, args);
            }
            else
            {
                controllertype = ControllerType.mPlayer;              
                CameraManage.Instance.SwitchCameraEffect(ECameraType.FOLLOW, cam, null, args);
             
            }
        }
        if (controllertype == ControllerType.mPlayer)//控制角色时
        {         
            if (Input.GetKeyDown(jump))
            {
                ZTEvent.FireEvent(EventID.REQ_PLAYER_JUMP);
            }
            if (Input.GetKeyDown(attack))
            {
                ZTEvent.FireEvent(EventID.REQ_PLAYER_Attack);
            }
            if (Input.GetKeyDown(changemainplayer))
            {
                if (LevelData.MainPlayer.Id == 1)
                    ZTEvent.FireEvent(EventID.REQ_PLAYER_Change, 2);
                else
                    ZTEvent.FireEvent(EventID.REQ_PLAYER_Change, 1);
            }
            if (Input.GetKeyDown(takestone))
            {
                ZTEvent.FireEvent(EventID.REQ_PLAYER_TakeStone);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "enemy")
                    {
                        ZTEvent.FireEvent(EventID.REQ_PLAYER_EnemyArea, hit.collider.gameObject.GetComponent<ActorBehavior>().Owner);
                    }
                    else if (hit.collider.tag == "Player")
                    {
                        if (hit.collider.gameObject.GetComponent<ActorBehavior>().Owner.Id == LevelData.MainPlayer.Id) return;
                        ZTEvent.FireEvent(EventID.REQ_PLAYER_Change, hit.collider.gameObject.GetComponent<ActorBehavior>().Owner.Id);
                    }
                    else
                    {
                        ZTEvent.FireEvent(EventID.REQ_PLAYER_ThrowingStone);
                    }
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                CameraManage.Instance.SetFollowDis(1);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                CameraManage.Instance.SetFollowDis(-1);
            }
            if (x != 0 || y != 0)
            {
                ZTEvent.FireEvent(EventID.REQ_PLAYER_Walk, x, y);
            }
            else
            {
                ZTEvent.FireEvent(EventID.REQ_PLAYER_Idle);
            }  
        }
        else//控制相机时
        {
            if (x != 0 || y != 0)
            {
                ZTEvent.FireEvent(EventID.REQ_Camera_Move, x, y);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "enemy")
                    {
                        ZTEvent.FireEvent(EventID.REQ_PLAYER_EnemyArea, hit.collider.gameObject.GetComponent<ActorBehavior>().Owner);
                    }                  
                }
            }
        }
         
    }
 

    
}

