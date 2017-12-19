/**********************************************
创建日期：2017/12/19 星期二 14:33:30
作者：张海城
说明:
**********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public  class UIIntroduce:BaseWindow
{
    private Text IntroduceText;
    public UIIntroduce() 
    {
        mResPath = "UI/Game/Introduce";
        Type = WindowType.DIALOG;
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

    protected override void InitWidget()
    {
        IntroduceText = this.transform.Find("Text").GetComponent<Text>();
        IntroduceText.DOText("5555555555555555555", 5).OnComplete(() =>
        {
            Close();
        });
    }
}

