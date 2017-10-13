using UnityEngine;
using System.Collections;

public class GameLoginState : GameBaseState
{
  
    public override void Enter()
    {
        base.Enter();
        UIManage.Instance.OpenWindow(WindowID.UI_LOGIN);
    }

    public override void Execute()
    {
        base.Execute();
        
    }

    public override void Exit()
    {
        base.Exit();
    }
    
	
	
	
}
