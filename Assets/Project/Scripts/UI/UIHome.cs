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
        //HpValue = transform.Find("HPBar/HpValue").GetComponent<Text>();
    }

    protected override void OnAddButtonListener()
    {
        
    }

    protected override void OnAddHandler()
    {
     
    }

    protected override void OnRemoveHandler()
    {
      
    }

    protected override void OnEnable()
    {
        
    }

    protected override void OnDisable()
    {
        
    }
}
