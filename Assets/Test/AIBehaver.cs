using UnityEngine;
using System.Collections;
public class AIBehaver : MonoBehaviour
{
    public Transform cube;
    public float Angle = 60;
    public float distance = 5f;
    void Update()
    {
        Quaternion r = transform.rotation;
        Vector3 f0 = (transform.position + (r * Vector3.forward) * distance);
        Debug.DrawLine(transform.position, f0, Color.red);

        Quaternion r0 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - Angle/2, transform.rotation.eulerAngles.z);
        Quaternion r1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Angle/2, transform.rotation.eulerAngles.z);

        Vector3 f1 = (transform.position + (r0 * Vector3.forward) * distance);
        Vector3 f2 = (transform.position + (r1 * Vector3.forward) * distance);

        Debug.DrawLine(transform.position, f1, Color.red);
        Debug.DrawLine(transform.position, f2, Color.red);
        Debug.DrawLine(f1, f2, Color.red);

        Vector3 point = cube.position;

        if (IsPointInCircularSector(point, transform.position))
        {
            Debug.Log("cube in this !!!");
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

}

