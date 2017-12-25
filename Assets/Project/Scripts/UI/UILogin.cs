using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILogin : BaseWindow
{
    private Dictionary<string, GameObject> dic = new Dictionary<string, GameObject>();
    private Stack<GameObject> history = new Stack<GameObject>();
    private GameObject[] allPanels;
    private GameObject[] allUINavigateBtn;
    public GameObject CurrentPanel;
    private Button[] LevelSelectBtn;
    private GameObject ScrollPanel;
    private Toggle[] toggleArray;
    private Image[] IsLook = new Image[32];
    private Image[] LeftStar = new Image[32];
    private Image[] RightStar = new Image[32];
    private Image[] CenterStar = new Image[32];
    public Text[] LevelNum = new Text[32];
    public UILogin()
    {

        mResPath = "UI/Login/UILogin";
        Type = WindowType.WINDOW;

    }
    protected override void InitWidget()
    {
        CurrentPanel = transform.Find("P_Main").gameObject;
        LevelSelectBtn = transform.Find("P_Player/BGFram/ScrollPanel/GridContent").GetComponentsInChildren<Button>();
        ScrollPanel = transform.Find("P_Player/BGFram/ScrollPanel").gameObject;
        toggleArray = transform.Find("P_Player/BGFram/ToggleGroup").GetComponentsInChildren<Toggle>();
        LevelButtonScrollRect levelButtonScrollRect = ScrollPanel.GET<LevelButtonScrollRect>();
        levelButtonScrollRect.toggleArray = toggleArray;
    }

    void InitLevel()
    {
        for (int i = 0; i < LevelSelectBtn.Length; i++)
        {
            IsLook[i] = LevelSelectBtn[CulPos(i)].transform.Find("Lock").gameObject.GetComponent<Image>();
            LeftStar[i] = LevelSelectBtn[CulPos(i)].transform.Find("StarLeft").gameObject.GetComponent<Image>();
            RightStar[i] = LevelSelectBtn[CulPos(i)].transform.Find("StarRight").gameObject.GetComponent<Image>();
            CenterStar[i] = LevelSelectBtn[CulPos(i)].transform.Find("StarCenter").gameObject.GetComponent<Image>();
            LevelNum[i] = LevelSelectBtn[CulPos(i)].transform.Find("Text").gameObject.GetComponent<Text>();
        }
        for (int i = 0; i < GameDataManage.Instance.LevelListDatas.Count; i++)
        {
            LevelItem levelItem = GameDataManage.Instance.GetLevelItemData(10001 + i);
            if (levelItem.isopen)
            {
                IsLook[i].gameObject.SetActive(false);
                LevelNum[i].gameObject.SetActive(true);
                LevelNum[i].text = (i + 1).ToString();
            }
            else
            {
                IsLook[i].gameObject.SetActive(true);
                LevelNum[i].text = (i + 1).ToString();
                LevelNum[i].gameObject.SetActive(false);
            }
            if (levelItem.star == 1)
            {
                LeftStar[i].gameObject.SetActive(true);
                CenterStar[i].gameObject.SetActive(false);
                RightStar[i].gameObject.SetActive(false);
            }
            else if (levelItem.star == 2)
            {
                LeftStar[i].gameObject.SetActive(true);
                CenterStar[i].gameObject.SetActive(true);
                RightStar[i].gameObject.SetActive(false);
            }
            else if (levelItem.star == 3)
            {
                LeftStar[i].gameObject.SetActive(true);
                CenterStar[i].gameObject.SetActive(true);
                RightStar[i].gameObject.SetActive(true);
            }
            else
            {
                LeftStar[i].gameObject.SetActive(false);
                CenterStar[i].gameObject.SetActive(false);
                RightStar[i].gameObject.SetActive(false);
            }
        }
    }

    int CulPos(int pos)
    {
        int j = pos / 8;
        int c = pos % 8;
        switch (c)
        {
            case 0:
                return 8 * j;
            case 1:
                return 2 + 8 * j;
            case 2:
                return 4 + 8 * j;
            case 3:
                return 6 + 8 * j;
            case 4:
                return 1 + 8 * j;
            case 5:
                return 3 + 8 * j;
            case 6:
                return 5 + 8 * j;
            case 7:
                return 7 + 8 * j;
            default:
                return 0;

        }

    }
    public void SelectLevel(GameObject go)
    {
        int LevelNum = Int32.Parse(go.transform.GetComponentInChildren<Text>().text);
        string ScenesName = "Level" + LevelNum;
        LevelData.SceneID =10000+ LevelNum;
        StartGame.Instance.LoadScene(LevelData.SceneID);
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
        else if (history.Count > 0)
        {
            newPanel = history.Pop();
        }
        if (newPanel != null && CurrentPanel != null)
        {
            CurrentPanel.SetActive(false);
            CurrentPanel = newPanel;
            CurrentPanel.SetActive(true);
        }
    }

    public void OnButtonClick(GameObject go)
    {
        ShowPanel(go.name.Substring(3));
        InitLevel();
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
