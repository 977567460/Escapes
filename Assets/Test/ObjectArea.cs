using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArea : MonoBehaviour {
    public float Width=5;
    public float Height = 5;
    public Transform Target;
  
   
	// Use this for initialization
	void Start () {
      
        
	}
	
	// Update is called once per frame
	void Update () {
        DrawArea();
        if (IsPointInPolygon(Width, Height, Target.position))
        {
            Target.GetComponent<PlayerInfo>().IsBunker = true;
        }
        else
        {
            Target.GetComponent<PlayerInfo>().IsBunker = false;        
        }
	}
    void DrawArea()
    {
        LineRenderer lr = this.GetComponent<LineRenderer>();
        if (lr == null)
        {

            lr = this.gameObject.AddComponent<LineRenderer>();

        }
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.SetVertexCount(5);
        lr.SetPosition(0, transform.position-new Vector3(Width/2,0,Height/2));
        lr.SetPosition(1, transform.position - new Vector3(-Width / 2, 0, Height / 2));
        lr.SetPosition(2, transform.position - new Vector3(-Width / 2, 0, -Height / 2));
        lr.SetPosition(3, transform.position - new Vector3(Width / 2, 0, -Height / 2));
        lr.SetPosition(4, transform.position - new Vector3(Width / 2, 0, Height / 2));
       
    }
    bool IsPointRect(Vector3 Target)
    {
       
        return false;
    }
    public bool IsPointInPolygon(float width,float height, Vector3 target)
    {
        float targetx = target.x;
        float targety = target.z;
        float OrignX1 = (transform.position - new Vector3(Width / 2, 0, Height / 2)).x;
        float OrignY1 = (transform.position - new Vector3(Width / 2, 0, Height / 2)).z;
        float OrignX2 = (transform.position - new Vector3(-Width / 2, 0, -Height / 2)).x;
        float OrignY2 = (transform.position - new Vector3(-Width / 2, 0, -Height / 2)).z;
        if ((targetx > OrignX1 && targetx < OrignX2) && (targety > OrignY1 && targety < OrignY2))
        {
            //inside  
            return true;
        }
        else
        {
            //outside  
            return false;
        }  
    }
}
