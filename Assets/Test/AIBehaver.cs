using UnityEngine;
using System.Collections;
public class AIBehaver : MonoBehaviour
{
    public Transform cube;
    public float Angle = 60;
    public float distance = 5f;
    public float ObjectRotateAngle = 60;
    private int Dir = 1;
    public float RotateSpeed = 60;
    void Update()
    {
        ObjectRotate();
        if (!cube.GetComponent<PlayerInfo>().IsBunker){
            if (IsPointInCircularSector(cube.position, transform.position))
            {
                Debug.Log("cube in this !!!");
            }
            else
            {
                Debug.Log("cube not in this !!!");
            }
        }
        else
        {
            Debug.Log("cube not in this !!!");
        }
       
        DrawSector(this.transform, transform.position, Angle, distance);
    }
    bool IsPointInCircularSector(Vector3 targetpoint, Vector3 Orign)
    {       
        targetpoint.y = 0;
        Orign.y = 0;
        Vector3 targetDir = targetpoint - Orign;
        float Dis= Vector3.Distance(targetpoint, Orign);
        if (distance > Dis)
        {
            if (Vector3.Angle(transform.forward, targetDir) < Angle / 2)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        else
        {
            return false;
        }
            
      
    }  
 
    public static void DrawSector(Transform t, Vector3 center, float angle, float radius)
    {
        LineRenderer lr = t.GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = t.gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        int pointAmount = 100;//点的数目，值越大曲线越平滑  
        float eachAngle = angle / pointAmount;
        Vector3 forward = t.forward;
        lr.SetVertexCount(pointAmount);
        lr.SetPosition(0, center);
        lr.SetPosition(pointAmount - 1, center);

        for (int i = 1; i < pointAmount - 1; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + eachAngle * (i - 1), 0f) * forward * radius + center;
            lr.SetPosition(i, pos);
        }

    }
    void ObjectRotate()
    {
        float Angle = transform.localEulerAngles.y;
        if (Angle > 180)
        {
            Angle = Angle - 360;
        }

        if (Angle >= ObjectRotateAngle/2)
        {
            Dir = -1;
        }
        if (Angle <= -ObjectRotateAngle/2)
        {
            Dir = 1;
        }

        transform.Rotate(Vector3.up, Dir * Time.deltaTime *RotateSpeed);
         
       // Debug.Log(transform.localEulerAngles.y);
    }

}

