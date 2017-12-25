using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BottleScript : MonoBehaviour
{
    public bool isDrag = true;
    public Actor actor;
    void Start()
    {
       
    }
    void Update()
    {
         Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);//实例化从摄像机到鼠标的摄像  
         RaycastHit hit;//这个为射线命中的点  
            if (Physics.Raycast (ray,out hit)) {//物理静态类中的光线投射方法 ->  意义:射线投射出  "得到射线命中的点(hit)"  
            if(!isDrag)return;
            if (JudgeCircle(hit.point,actor.GetAttr(EAttr.ViewLength)))
            this.transform.position = hit.point;                         
        } 
        
    }
    bool JudgeCircle(Vector3 point,float dis)
    {
      float tempDis= Vector3.Distance(actor.CacheTransform.position, point);
      if (tempDis > dis)
      {
          return false;
      }
      return true;
    }
}

