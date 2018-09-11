using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

 public class UIDialog:BaseWindow
 {
     public Text Content;
     public Text Name;
     public Image Headr;
     public float texttime=5;

    public UIDialog()
    {
        mResPath = "UI/Game/Dialog";
        Type = WindowType.DIALOG;
    }
    protected override void InitWidget()
    {
        Headr = transform.Find("Head").GetComponent<Image>();
        Content = transform.Find("Content").GetComponent<Text>();
        Name = transform.Find("Name").GetComponent<Text>();

     
    }
    public void showText(string Name, Sprite header, string context)
    {
        this.Name.text = Name;
        this.Headr.sprite = header;
        Content.DOText(context, texttime);
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

