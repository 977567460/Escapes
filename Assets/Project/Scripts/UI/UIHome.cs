using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHome : BaseWindow
{
    private Slider HpSlider;
    private Text HpValue;
    public UIHome()
    {
        mResPath = "UI/Game/Home";
        Type = WindowType.WINDOW;
    }
    protected override void InitWidget()
    {
        HpSlider = transform.Find("HPBar").GetComponent<Slider>();
        HpValue = transform.Find("HPBar/HPValue").GetComponent<Text>();
        HpSlider.value = LevelData.MainPlayer.GetAttr(EAttr.HP) / LevelData.MainPlayer.GetAttr(EAttr.MaxHP);
        HpValue.text = LevelData.MainPlayer.GetAttr(EAttr.HP).ToString();
    }

    protected override void OnAddButtonListener()
    {

    }

    protected override void OnAddHandler()
    {
        ZTEvent.AddHandler(EventID.REQ_PLAYER_Attr, InitWidget);
    }

    protected override void OnRemoveHandler()
    {
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_Attr, InitWidget);
    }

    protected override void OnEnable()
    {

    }

    protected override void OnDisable()
    {

    }
}
