using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*******************************************************
 * Class:           AIConeDetection
 * Description:     Description begin here
 * 
 * Studio Leaves (c) 
 *******************************************************/

public class AIConeDetection : MonoBehaviour
{
    public GameObject player;
    /* Fov Properties */
    public bool m_bIs2D = false;
    public float m_fConeLenght = 5.0f;
    public float m_fAngleOfView = 90.0f;
    private float[] m_fAngleOfViewlist = new float[SectorCount];
    public float m_vStartDistanceCone = 2.0f;
    private Material m_matVisibilityCone = null;
    public bool m_bHasStartDistance = true;
    public int m_LayerMaskToIgnoreBegin = 0;
    public int m_LayerMaskToIgnoreEnd = 0;
    private int m_LayerMaskToIgnore = ~(1 << 8);
    //public  float       m_fFixedCheckInterval       = 0.5f;
    private float m_fFixedCheckNextTime;

    /* Render Properties */
    public bool m_bShowCone = true;
    public int m_iConeVisibilityPrecision = 60;
    //public  float       m_fDistanceForRender        = 600.0f;

    
    private Vector3[] m_vVertices;
    private Vector2[] m_vUV;
    private Vector3[] m_vNormals;
    private int[] m_iTriangles;
    private GameObject[] m_goVisibilityCone = new GameObject[SectorCount];
    private int m_iVertMax = 120;
    private int m_iTrianglesMax = 120;

    private float[] m_fSpan = new float[SectorCount];
    private float[] m_fStartRadians = new float[SectorCount];
    private float[] m_fCurrentRadians = new float[SectorCount];
    public static int SectorCount = 10;

    //private float 		m_fConeLenghtFixed;
    private List<Vector3> SevtorVertexCount = new List<Vector3>();

    private ArrayList[] m_goGameObjectIntoCone = new ArrayList[SectorCount];
    private List<GameObject> FindObjList = new List<GameObject>();
    private GameObject GroundMesh;
    private Material GroundMat;
    Vector3[] OutVertices;
    Vector3[] InnerVertices;
    public float StartAngle = -30;
    private float IntervalAngle;
    public float EndAngle = -80;
    private float StartRadians;
    private float IntervalRadians;
    private float EndRadians;
    private bool IsEnter = false;
    public ArrayList[] GameObjectIntoCone
    {
        get { return m_goGameObjectIntoCone; }
    }

    public bool Is2DCone
    {
        get { return m_bIs2D; }
    }

    void Start()
    {
        GroundMat = new Material(Shader.Find("Mobile/Particles/Additive"));
        m_matVisibilityCone = GroundMat;
        m_LayerMaskToIgnore = ~(m_LayerMaskToIgnoreBegin << m_LayerMaskToIgnoreEnd);
        InitAIConeDetection();
    }

    void Update()
    {
        UpdateAIConeDetection();
    }

    private void InitAIConeDetection()
    {
        GroundMesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GroundMesh.name = this.name+"Mesh";
        GroundMesh.GetComponent<Renderer>().material = GroundMat;
        if (GroundMesh.GetComponent<BoxCollider>()!=null)
        Destroy(GroundMesh.GetComponent<BoxCollider>());
        for (int j = 0; j < SectorCount; j++)
        {
            m_fAngleOfViewlist[j] = m_fAngleOfView;
            m_goGameObjectIntoCone[j] = new ArrayList();
            
        
            m_iVertMax = m_iConeVisibilityPrecision * 2 + 2;
            m_iTrianglesMax = m_iConeVisibilityPrecision * 2;
            m_vVertices = new Vector3[m_iVertMax];
            m_iTriangles = new int[m_iTrianglesMax * 3];
            m_vNormals = new Vector3[m_iVertMax];
       
            for (int i = 0; i < m_iVertMax; ++i)
            {
                m_vNormals[i] = Vector3.up;
            }
            m_fStartRadians[j] = (360.0f - (m_fAngleOfViewlist[j])) * Mathf.Deg2Rad;
            m_fCurrentRadians[j] = m_fStartRadians[j];
            m_fSpan[j] = (m_fAngleOfViewlist[j]) / m_iConeVisibilityPrecision;
            m_fSpan[j] *= Mathf.Deg2Rad;
            m_fSpan[j] *= 2.0f;
            m_fAngleOfViewlist[j] *= 0.5f;
        }
        
        InnerVertices=new Vector3[m_iVertMax/2];
        OutVertices = new Vector3[m_iVertMax/2];
        IntervalAngle = (EndAngle - StartAngle) / SectorCount;
        StartRadians = StartAngle * Mathf.Deg2Rad;
        EndRadians = EndAngle * Mathf.Deg2Rad;
        IntervalRadians = IntervalAngle * Mathf.Deg2Rad;
        //m_fConeLenghtFixed  = m_fConeLenght * m_fConeLenght;
    }

    private void UpdateAIConeDetection()
    {
        DrawVisibilityCone2();
        if (FindObj())
        {
            if (!IsEnter)
            {
              //  player.GetComponent<MeshRenderer>().material.color = Color.red;
                IsEnter = true;
                Debug.Log("进");
            }


        }
        else
        {
            if (IsEnter)
            {
              //  player.GetComponent<MeshRenderer>().material.color = Color.green;
                IsEnter = false;
                Debug.Log("出");
            }

        }
    }


    bool FindObj()
    {
        FindObjList.Clear();
        for (int i = 0; i < GameObjectIntoCone.Length; i++)
        {
            for (int j = 0; j < GameObjectIntoCone[i].Count; j++)
            {
                FindObjList.Add((GameObject)GameObjectIntoCone[i][j]);
            }

        }
        if (FindObjList.Contains(player))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private RaycastHit m_rcInfo;
    private Ray m_rayDir = new Ray();
   // Vector3 EnemyBeginPos;
 
    private void DrawVisibilityCone2()
    {
        SevtorVertexCount.Clear();
        for (int Sector = 0; Sector < SectorCount; Sector++)
        {

            m_goGameObjectIntoCone[Sector].Clear();


            m_fCurrentRadians[Sector] = m_fStartRadians[Sector];
            Vector3 CurrentVector = this.transform.forward;
            Vector3 DrawVectorCurrent = this.transform.forward;

            int index = 0;
            int My_index = 0;
            int My_index2 = 0;
            // EnemyBeginPos = new Vector3(transform.position.x, 0, transform.position.z);
           // SevtorVertexCount.Add(EnemyBeginPos);
            for (int i = 0; i < m_iConeVisibilityPrecision + 1; ++i)
            {

                if (!m_bIs2D)
                {
                    float newX = CurrentVector.x * Mathf.Cos(m_fCurrentRadians[Sector]) -
                                 CurrentVector.z * Mathf.Sin(m_fCurrentRadians[Sector]);
                    float newZ = CurrentVector.x * Mathf.Sin(m_fCurrentRadians[Sector]) +
                                 CurrentVector.z * Mathf.Cos(m_fCurrentRadians[Sector]);
                    float newY = StartRadians + Sector * IntervalRadians;

                    DrawVectorCurrent.x = newX;
                    DrawVectorCurrent.y = newY;
                    DrawVectorCurrent.z = newZ;
                }
                else
                {
                    float newX = CurrentVector.x * Mathf.Cos(m_fCurrentRadians[Sector]) -
                                 CurrentVector.y * Mathf.Sin(m_fCurrentRadians[Sector]);
                    //float newZ = CurrentVector.y * Mathf.Sin( m_fCurrentRadians ) + CurrentVector.z * Mathf.Cos( m_fCurrentRadians );
                    float newY = CurrentVector.x * Mathf.Sin(m_fCurrentRadians[Sector]) +
                                 CurrentVector.y * Mathf.Cos(m_fCurrentRadians[Sector]);

                    DrawVectorCurrent.x = newX;
                    DrawVectorCurrent.y = newY;
                    DrawVectorCurrent.z = 0.0f;
                }
                m_fCurrentRadians[Sector] += m_fSpan[Sector];
                /* Calcoliamo dove arriva il Ray */
                float FixedLenght = m_fConeLenght;
                bool bFoundWall = false;
                /* Adattiamo la mesh alla superfice sulla quale tocca */

                m_rayDir.origin = this.transform.position;
                m_rayDir.direction = DrawVectorCurrent.normalized;

                /* If we have the 2D support, we should check for 2D colliders */
                if (m_bIs2D)
                {

                    Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);
                    Vector2 dir = new Vector2(m_rayDir.direction.x, m_rayDir.direction.y);

                    RaycastHit2D hit = Physics2D.Raycast(pos, dir, Mathf.Infinity, m_LayerMaskToIgnore);
                    if (hit.collider != null)
                    {
                        if (hit.distance < m_fConeLenght)
                        {
                            bFoundWall = true;
                            FixedLenght = hit.distance;

                            bool bGOFound = false;
                            foreach (GameObject go in m_goGameObjectIntoCone[Sector])
                            {
                                if (go.GetInstanceID() == hit.collider.gameObject.GetInstanceID())
                                {
                                    bGOFound = true;
                                    break;
                                }
                            }
                            if (!bGOFound)
                            {
                                m_goGameObjectIntoCone[Sector].Add(hit.collider.gameObject);
                            }
                        }
                    }
                }

                if (Physics.Raycast(m_rayDir, out m_rcInfo, Mathf.Infinity, m_LayerMaskToIgnore))
                {
                    if (m_rcInfo.distance < m_fConeLenght)
                    {
                        bFoundWall = true;
                        FixedLenght = m_rcInfo.distance;

                        bool bGOFound = false;
                        foreach (GameObject go in m_goGameObjectIntoCone[Sector])
                        {
                            if (go.GetInstanceID() == m_rcInfo.collider.gameObject.GetInstanceID())
                            {
                                bGOFound = true;
                                break;
                            }
                        }
                        if (!bGOFound)
                        {
                            m_goGameObjectIntoCone[Sector].Add(m_rcInfo.collider.gameObject);
                        }
                    }
                }

                if (m_bHasStartDistance)
                {
                    m_vVertices[index] = this.transform.position + DrawVectorCurrent.normalized * m_vStartDistanceCone;
                }
                else
                {
                    m_vVertices[index] = this.transform.position;
                }

                m_vVertices[index + 1] = this.transform.position + DrawVectorCurrent.normalized * FixedLenght;


                //m_vVertices[ index + 1 ].y  = this.transform.position.y;

                Color color;
                Vector3 tempvertex;
             
                if (bFoundWall)
                {
                    color = Color.red;                            
                }
                else
                {
                    color = Color.yellow;                                   
                }

                tempvertex = this.transform.position + DrawVectorCurrent.normalized * FixedLenght;
                tempvertex = new Vector3(tempvertex.x, 0.01f, tempvertex.z);
                if (Sector == 0)
                {
                  //  SevtorVertexCount.Add(tempvertex);
                    OutVertices[My_index] = tempvertex;
                   
                    My_index += 1;
                    
                }
                if( Sector == SectorCount - 1){
                  //  SevtorVertexCount.Add(tempvertex);
                    InnerVertices[My_index2] = tempvertex;
                    My_index2 += 1;
                   
                }
                
               
                index += 2;
            }
           

        }
       // DrawLine();
        DrawMesh();
    }
    void DrawLine()
    {

        for (int i = 0; i < InnerVertices.Length; i++)
        {
            SevtorVertexCount.Add(InnerVertices[i]);                    
        }     
        for (int i = OutVertices.Length-1; i >= 0; i--)
        {
            SevtorVertexCount.Add(OutVertices[i]);
            
        }
       SevtorVertexCount.Add(InnerVertices[0]);
        LineRenderer line = this.GetComponent<LineRenderer>();
        line.SetColors(Color.red, Color.red);
        //设置宽度  
        line.SetWidth(0.02f, 0.02f);
        line.SetVertexCount(SevtorVertexCount.Count);
        // line.SetPosition(0, tempAiTran);
        //连续绘制线段  
       
        for (int j = 0; j < SevtorVertexCount.Count; j++)
        {
            line.SetPosition(j, SevtorVertexCount[j]);

        }           
    }
    void DrawMesh()
    {
      //  GroundMesh.transform.position = this.transform.position;
       // GroundMesh.transform.eulerAngles = this.transform.eulerAngles;

        GroundMesh.GetComponent<MeshFilter>().mesh = CreateMesh(m_fConeLenght, 2,(180-m_fAngleOfView)/2, m_fAngleOfView, m_iConeVisibilityPrecision/2);
    }
    Mesh CreateMesh(float radius, float innerradius,float startangle, float angledegree, int segments)
    {
        //vertices(顶点):
        int vertices_count = segments * 2 + 2;              //因为vertices(顶点)的个数与triangles（索引三角形顶点数）必须匹配
        Vector3[] vertices = new Vector3[vertices_count];
        float angleRad = Mathf.Deg2Rad * angledegree;
        float StartangleRad = Mathf.Deg2Rad * startangle;
        float angleCur = angleRad + StartangleRad;
        float angledelta = angleRad / segments;
        for (int i = 0; i < vertices_count; i += 2)
        {          
            vertices[i] = OutVertices[i];
            vertices[i + 1] = InnerVertices[i];
            angleCur -= angledelta;
        }

        //triangles:
        int triangle_count = segments * 6;
        int[] triangles = new int[triangle_count];
        for (int i = 0, vi = 0; i < triangle_count; i += 6, vi += 2)
        {
            triangles[i] = vi;
            triangles[i + 1] = vi + 3;
            triangles[i + 2] = vi + 1;
            triangles[i + 3] = vi + 2;
            triangles[i + 4] = vi + 3;
            triangles[i + 5] = vi;
        }

        //uv:
        Vector2[] uvs = new Vector2[vertices_count];
        for (int i = 0; i < vertices_count; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f, vertices[i].z / radius / 2 + 0.5f);
        }
     
        //负载属性与mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
