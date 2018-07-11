using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

    public class SetHp : MonoBehaviour
    {
        public Text TalkText;
        private Canvas _Canvas;
        public Actor owner;
        private float destroytime = 5f;
        void Start()
        {

            _Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
 
        }

        void Update()
        {

            Vector2 position;
            Vector3 screenPosition = CameraManage.Instance.MainCamera.WorldToScreenPoint(owner.mActorPart.HpTransform.position);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_Canvas.transform, screenPosition,
               _Canvas.worldCamera, out position))
            {
                this.GetComponent<RectTransform>().anchoredPosition = position;
            }
        }
   
       
    }

