using UnityEngine;
using System.Collections;

public class GameBattleState :GameBaseState {
    public override void Enter()
    {
        base.Enter();
        UIManage.Instance.OpenWindow(WindowID.UI_HOME);
    }
}
