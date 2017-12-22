using UnityEngine;
using System.Collections;

public class GameBattleState :GameBaseState {
    public override void Enter()
    {
        base.Enter();
        UIManage.Instance.OpenWindow(WindowID.UI_HOME);
        UIManage.Instance.OpenWindow(WindowID.UI_INTRODUCE);
        AudioClip clip = LoadResource.Instance.Load<AudioClip>("Sounds/TEBASAKI");
        ZTAudio.Instance.PlayMusic(clip);
    }
}
