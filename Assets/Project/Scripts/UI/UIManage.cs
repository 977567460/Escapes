using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum WindowID
{
    UI_START,
    UI_LOGIN,
    UI_REGISTER,
    UI_SERVER,
    UI_NOTICE,
    UI_CREATEROLE,
    UI_HOME,
    UI_TALK,
    UI_MESSAGE,
    UI_SURETOBUY,
    UI_HEROINFO,
    UI_BAG,
    UI_ITEMINFO,
    UI_ITEMUSE,
    UI_EQUIPINFO,
    UI_GEMINFO,
    UI_CHIPINFO,
    UI_FASHIONINFO,
    UI_RUNEINFO,
    UI_INTRODUCE,
    UI_ROLEEQUIP,
    UI_ROLEGEM,
    UI_ROLEFASHION,
    UI_ROLERUNE,
    UI_ROLEFETTER,

    UI_EQUIP,
    UI_GEM,
    UI_AWARD,

    UI_MAINRAID,
    UI_MAINGATE,
    UI_MAINRESULT,

    UI_AWARDBOX,
    UI_LOADING,

    UI_PET,

    UI_PARTNER,
    UI_PARTNERADVANCE,
    UI_PARTNERSTRENGH,
    UI_PARTNERSTAR,
    UI_PARTNERWAKE,
    UI_PARTNERWASH,
    UI_PARTNERSKILL,
    UI_PARTNERFETTER,
    UI_PARTNERFPROPERTY,
    UI_PARTNERBATTLE,

    UI_MOUNT,
    UI_MOUNTLIBRARY,
    UI_MOUNTBLOOD,
    UI_MOUNTTAME,
    UI_MOUNTTURNED,


    UI_RELICS,
    UI_RELICSSKILL,

    UI_MASK,

    UI_STORE,
    UI_TASK,
    UI_WORLDMAP,

    UI_GUIDE,
    UI_TASKTALK,
    UI_TASKINTERACTIVE,
    UI_PLOTCUTSCENE,

    UI_ADVENTURE,
    UI_SKILL,
    UI_REBORN,
    UI_FLUTTER,
    UI_BOARD,

}
public enum WindowType
{
    BOTTOM,
    FLYWORD,
    WINDOW,
    DIALOG,
    MSGTIP,
    LOADED,
    MASKED,
}
public class UIManage : Singleton<UIManage>, IGame
{
    public UIManage()
    {
        RegisterWindow(WindowID.UI_LOGIN, new UILogin());
        RegisterWindow(WindowID.UI_LOADING, new UILoading());
        RegisterWindow(WindowID.UI_HOME, new UIHome());
        RegisterWindow(WindowID.UI_INTRODUCE, new UIIntroduce());
       
    }
    public Dictionary<WindowID, BaseWindow> mAllWindows = new Dictionary<WindowID, BaseWindow>();
    public Dictionary<WindowType, List<BaseWindow>> mOpenWindows = new Dictionary<WindowType, List<BaseWindow>>();
    public List<BaseWindow> mOpenWinStack = new List<BaseWindow>();
    void RegisterWindow(WindowID id, BaseWindow win)
    {
        mAllWindows[id] = win;
        win.ID = id;
    }
    public BaseWindow OpenWindow(WindowID windowID)
    {
        if (!mAllWindows.ContainsKey(windowID))
        {
            return null;
        }

        BaseWindow window = mAllWindows[windowID];
        DealWindowStack(window, true);
       
        window.Show();
        Transform trans = window.CacheTransform;
        CameraManage.Instance.AddUI(trans.gameObject);
        List<BaseWindow> list = null;
        mOpenWindows.TryGetValue(window.Type, out list);
        if (list == null)
        {
            list = new List<BaseWindow>();
            mOpenWindows[window.Type] = list;
        }
        list.Add(window);
        return window;
    }
    public BaseWindow GetWindow(WindowID windowID)
    {
        BaseWindow window = null;
        mAllWindows.TryGetValue(windowID, out window);
        return window;
    }
    void DealWindowStack(BaseWindow win, bool open)
    {
        if (win.Type != WindowType.WINDOW)
        {
            return;
        }
        if (open)
        {
            for (int i = 0; i < mOpenWinStack.Count; i++)
            {
                if (mOpenWinStack[i] != win)
                {
                    mOpenWinStack[i].CacheTransform.gameObject.SetActive(false);
                }
            }
            mOpenWinStack.Add(win);
        }
        else
        {
            mOpenWinStack.Remove(win);
            if (mOpenWinStack.Count > 0)
            {
                BaseWindow last = mOpenWinStack[mOpenWinStack.Count - 1];
                last.CacheTransform.gameObject.SetActive(true);
            }
        }
    }
    public void CloseWindow(WindowID windowID)
    {
        BaseWindow window = mAllWindows[windowID];
        if (window == null) return;
        WindowType type = window.Type;
        window.Close();
        List<BaseWindow> list = null;
        mOpenWindows.TryGetValue(type, out list);
        if (list != null)
        {
            list.Remove(window);
        }
        DealWindowStack(window, false);
    }
    public void Clear()
    {     
        foreach (KeyValuePair<WindowID, BaseWindow> pair in mAllWindows)
        {
            if (pair.Key != WindowID.UI_LOADING)
            {
                pair.Value.Close();
            }
        }
        mOpenWindows.Clear();
        mOpenWinStack.Clear();
    }
    public void Start()
    {

    }


    public void Step()
    {
        Dictionary<WindowID, BaseWindow>.Enumerator em = mAllWindows.GetEnumerator();
        while (em.MoveNext())
        {
            BaseWindow w = em.Current.Value;
            if (w.CacheTransform != null && w.CacheTransform.gameObject.activeSelf && w.IsVisable())
            {
                em.Current.Value.Update(Time.deltaTime);
            }
        }
        em.Dispose();
    }
  
}
