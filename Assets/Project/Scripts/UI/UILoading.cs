using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILoading : BaseWindow {
    public Slider LoadingSlider;
    public UILoading()
    {
        Type = WindowType.LOADED;
       
        mResPath = "UI/Loading/Loading";
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
        UpdateSliderValue(0);
    }

    protected override void OnDisable()
    {
        
    }

    protected override void InitWidget()
    {
        LoadingSlider = transform.Find("Slider").GetComponent<Slider>();
    }

    public  void UpdateSliderValue(float progress)
    {
        LoadingSlider.value = progress;
    }
}
