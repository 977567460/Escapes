using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoadingState : GameBaseState {
     public float mWaitTime = 0.1f;
    public float mWaitTimer = 0f;
    public bool mLoading = false;
    public UILoading mLoadingWindow;
    public AsyncOperation mAsync;
    public int mLoadingSceneId;
    public override void Enter()
    {
        base.Enter();
        GLCommand ev = Cmd as GLCommand;
        mLoadingSceneId = ev.SceneID;
        UIManage.Instance.Clear();
        UIManage.Instance.OpenWindow(WindowID.UI_LOADING);         
        mLoadingWindow = (UILoading) UIManage.Instance.GetWindow(WindowID.UI_LOADING);
        
    }

    public override void Execute()
    {
        if (mLoading == false)
        {
            if (mWaitTimer < mWaitTime)
            {
                mWaitTimer += Time.deltaTime;
            }
            else
            {
                ZTCoroutinue.Instance.StartCoroutine(LoadSnene());
                mLoading = true;
            }
        }

        if (mAsync != null)
        {
            if (mLoadingWindow.CacheTransform != null)
            {
                mLoadingWindow.UpdateSliderValue(mAsync.progress);
            }
            if (mAsync.isDone)
            {
                mLoading = false;
                mAsync = null;
                OnSceneWasLoaded();
            }
        }
    }

    public override void Exit()
    {
         
        OnLoadingWasFadeOut();
        mWaitTimer = 0;
        mLoading = false;
        mAsync = null;
    }
    public void OnSceneWasLoaded()
    {
        SceneData db = GameDataManage.Instance.GetDBScene(mLoadingSceneId);
       // AudioClip music = GameDataManage.Instance.Load<AudioClip>(db.SceneMusic);
        //ZTAudio.Instance.PlayMusic(music);
      //  ZTLevel.Instance.EnterWorld(mLoadingSceneId);
        StartGame.Instance.ChangeState(StartGame.Instance.NextState, Cmd);
    }
    public void OnLoadingWasFadeOut()
    {
        UIManage.Instance.CloseWindow(WindowID.UI_LOADING);
    }
    IEnumerator LoadSnene()
    {
        if (mAsync == null)
        {
            mAsync = LoadLevelById(mLoadingSceneId);
        }
        mAsync.allowSceneActivation = false;
        while (mAsync.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }
        mAsync.allowSceneActivation = true;
    }
    public AsyncOperation LoadLevelById(int id)
    {
        SceneData db = GameDataManage.Instance.GetDBScene(id);      
        if (string.IsNullOrEmpty(db.SceneName))
        {
            return null;
        }
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        return SceneManager.LoadSceneAsync(db.SceneName);
    }
}
