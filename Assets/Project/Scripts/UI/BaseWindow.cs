using UnityEngine;
using System.Collections;

public abstract class BaseWindow {

    protected Transform transform;
    protected bool mVisable = false;
    protected string mResPath = string.Empty;
    public bool HasParentWindow { get; private set; }
    protected abstract void OnAddButtonListener();
    protected abstract void OnAddHandler();
    protected abstract void OnRemoveHandler();
    protected abstract void OnEnable();
    protected abstract void OnDisable();
    protected abstract void InitWidget();
    public WindowID ID { get; set; }
    public WindowType Type { get; protected set; }
    public bool IsVisable()
    {
        return mVisable;
    }
    public Transform CacheTransform
    {
        get { return transform; }
    }
    public bool Load()
    {
        if (string.IsNullOrEmpty(mResPath))
        {
            Debug.LogError("资源名为空");
            return false;
        }
        GameObject go = LoadResource.Instance.LoadWindow(mResPath);
        if (go == null)
        {
            Debug.LogError(string.Format("加载Window资源失败:{0}", mResPath));
            return false;
        }
        transform = go.transform;
        transform.gameObject.SetActive(false);
        return true;
    }
    public void Show()
    {
        if (transform == null)
        {
            if (Load())
            {
                InitWidget();
            }
         
        }
        OnAddButtonListener();
        if (transform)
        {
            transform.gameObject.SetActive(true);
            OnAddHandler();
            OnEnable();
        }
        mVisable = true;
    }
    public void Close()
    {
        if (transform)
        {
            mVisable = false;
            OnRemoveHandler();
            OnDisable();
           transform.gameObject.SetActive(false);        
        }
        HasParentWindow = false;
    }
    public void SetParent(BaseWindow parent)
    {
        if (transform == null)
            return;
        if (parent == null || parent.transform == null)
            return;
        transform.parent = parent.transform;
        HasParentWindow = true;
    }
    public virtual void Update(float deltaTime)
    {

    }

}
