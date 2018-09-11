using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

    public class SetHp : MonoBehaviour
    {
     
        private Canvas _Canvas;
        public Actor owner;
        public float Hpper;
        private float destroytime = 5f;
        private Slider hpslider;
        private GameObject Deadobj;
        void Start()
        {

            _Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            hpslider = this.transform.Find("Slider").gameObject.GetComponent<Slider>();
            Deadobj = this.transform.Find("Dead").gameObject;
            SetDeadUI(false);
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
            Hpper = owner.GetCurrAttr().HP / owner.GetCurrAttr().MaxHP;
            hpslider.value = Hpper;
        }
        public void SetDeadUI(bool dead)
        {
            Deadobj.gameObject.SetActive(dead);
        }
       
    }

