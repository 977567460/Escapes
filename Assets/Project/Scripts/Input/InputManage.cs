using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class InputManage : MonoSingleton<InputManage>
{
    public KeyCode jump { get; set; }
    public KeyCode attack { get; set; }
   
    void Start()
    {
        jump = KeyCode.Space;
        attack = KeyCode.E;
    
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

