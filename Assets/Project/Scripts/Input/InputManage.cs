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
    public KeyCode takestone { get; set; }
    RaycastHit hit;
    void Start()
    {
        jump = KeyCode.Space;
        attack = KeyCode.E;
        changemainplayer=KeyCode.Q;
        takestone = KeyCode.R;
    
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
            if(LevelData.MainPlayer.Id==1)
            ZTEvent.FireEvent(EventID.REQ_PLAYER_Change,2);
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
                else  if (hit.collider.tag == "Player")
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
            ZTEvent.FireEvent(EventID.REQ_PLAYER_Walk,x,y);
        }
        else
        {
            ZTEvent.FireEvent(EventID.REQ_PLAYER_Idle);
        }  
    }
 

    
}

