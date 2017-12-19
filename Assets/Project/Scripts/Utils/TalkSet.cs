/**********************************************
创建日期：2017/12/19 星期二 10:44:18
作者：张海城
说明:
**********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class TalkSet : MonoBehaviour
{
    public Text TalkText;
    private Canvas _Canvas;
    public Actor murderer;
    private float destroytime = 5f;
    void Start()
    {
          
        _Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        StartCoroutine(DestroyTalk());
    }

    void Update()
    {

        Vector2 position;
        Vector3 screenPosition = CameraManage.Instance.MainCamera.WorldToScreenPoint(murderer.mActorPart.TalkTransform.position);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_Canvas.transform, screenPosition,
           _Canvas.worldCamera, out position))
        {
            this.GetComponent<RectTransform>().anchoredPosition = position;
        }
    }

    public void SetText(string talk)
    {
        TalkText.text = talk;
    }

    IEnumerator DestroyTalk()
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(this.gameObject);
    }
}

