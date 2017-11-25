using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class InputManage : MonoSingleton<InputManage>
{
    public KeyCode jump { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
   
    void Start()
    {
        SetButton();
        ZTEvent.AddHandler(EventID.REQ_PLAYER_JUMP, OnJump);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_LEFT, OnLeft);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_BACKWARD, OnBack);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_FORWARD, OnForward);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_RIGHT, OnRight);
    }

    void Update()
    {
        if (Input.GetKeyDown(jump))
        {
            ZTEvent.FireEvent(EventID.REQ_PLAYER_JUMP);
        }
        if (Input.GetKeyDown(forward))
        {
            ZTEvent.FireEvent(EventID.REQ_PLAYER_FORWARD);
        }
        if (Input.GetKeyDown(backward))
        {
           ZTEvent.FireEvent(EventID.REQ_PLAYER_BACKWARD);
        }
        if (Input.GetKeyDown(left))
        {
            ZTEvent.FireEvent(EventID.REQ_PLAYER_LEFT);
        }
        if (Input.GetKeyDown(right))
        {
            ZTEvent.FireEvent(EventID.REQ_PLAYER_RIGHT);
        }
    }
    void SetButton()
    {
        jump = KeyCode.Space;
        forward = KeyCode.W;
        backward = KeyCode.S;
        left = KeyCode.A;
        right = KeyCode.D;
    }

    void OnJump()
    {
        
    }
    void OnLeft()
    {

    }
    void OnRight()
    {

    }
    void OnBack()
    {

    }
    void OnForward()
    {

    }
}

