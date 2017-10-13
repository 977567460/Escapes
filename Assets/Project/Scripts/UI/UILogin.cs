using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILogin : BaseWindow
{
  private Dictionary<string,GameObject> dic=new Dictionary<string, GameObject>(); 
    private Stack<GameObject> history=new Stack<GameObject>();
    private GameObject[] allPanels;
    private GameObject[] allUINavigateBtn;
    public GameObject CurrentPanel;
    private Button[] LevelSelectBtn;
    
    public UILogin()
    {
      
        mResPath = "UI/Login/UILogin";
        Type = WindowType.WINDOW;
  
    }
    protected override void InitWidget()
    {
        CurrentPanel = transform.Find("P_Main").gameObject;
        LevelSelectBtn =
            transform.Find("P_Player/BGFram/ScrollPanel/GridContent").GetComponentsInChildren<Button>();
      
    }

    public void SelectLevel(GameObject go)
    {
        int LevelNum = Int32.Parse(go.transform.GetComponentInChildren<Text>().text);
        string ScenesName = "Level" + LevelNum;
       
        StartGame.Instance.LoadScene(10001);
        UIManage.Instance.CloseWindow(WindowID.UI_LOGIN);
       // Application.LoadLevel(ScenesName);
    }
    public void ShowPanel(string panelName)
    {
        GameObject newPanel = null;
        if (panelName != "Back")
        {
            if (dic.ContainsKey(panelName))
            {
                history.Push(CurrentPanel);
                newPanel = dic[panelName];
            }
        }
        else if(history.Count>0)
        {
            newPanel = history.Pop();
        }
        if (newPanel!=null&&CurrentPanel!=null)
        {
            CurrentPanel.SetActive(false);
            CurrentPanel = newPanel;
            CurrentPanel.SetActive(true);
        }
    }

    public void OnButtonClick(GameObject go)
    {
        ShowPanel(go.name.Substring(3));
    }
    protected override void OnAddButtonListener()
    {
        foreach (var item in LevelSelectBtn)
        {
            EventTriggerListener.Get(item.gameObject).onClick += SelectLevel;
        }
    }

   
    protected override void OnAddHandler()
    {
       
    }

    protected override void OnRemoveHandler()
    {
       
    }

    protected override void OnEnable()
    {     
        allPanels = GameObject.FindGameObjectsWithTag("UIFunPanel");
        allUINavigateBtn = GameObject.FindGameObjectsWithTag("UINavigateBtn");
        foreach (var item in allPanels)
        {
            dic.Add(item.name.Substring(2), item);
            if (item != CurrentPanel)
                item.SetActive(false);
        }
        foreach (var item in allUINavigateBtn)
        {
            EventTriggerListener.Get(item).onClick += OnButtonClick;
        }
    }

    protected override void OnDisable()
    {
        
    }

   
}
