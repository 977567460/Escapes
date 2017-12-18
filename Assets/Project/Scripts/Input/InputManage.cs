using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class InputManage : MonoSingleton<InputManage>
{
    public KeyCode jump { get; set; }
    public KeyCode attack { get; set; }
    public KeyCode changemainplayer { get; set; }
    public KeyCode dragenemy { get; set; }
    void Start()
    {
        jump = KeyCode.Space;
        attack = KeyCode.E;
        changemainplayer=KeyCode.Q;
        dragenemy = KeyCode.R;
    
    }
    public override void SetDontDestroyOnLoad(Transform parent)
    {
        base.SetDontDestroyOnLoad(parent);

    }
    void Update()
    {
         float x = Input.GetAxis("Horizontal");
         float y = Input.GetAxis("Vertical");

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
            ZTEvent.FireEvent(EventID.REQ_PLAYER_Change);
        }
        if (Input.GetKey(dragenemy))
        {
            ZTEvent.FireEvent(EventID.REQ_PLAYER_DragEnemy);
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
            ZTEvent.FireEvent(EventID.REQ_PLAYER_Walk,x,y);
        }
        else
        {
            ZTEvent.FireEvent(EventID.REQ_PLAYER_Idle);
        }  
    }
 

    
}

