using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {
    public Vector3 screenSpace;
    public Vector3 offset;
    public RaycastHit hit;

    // Boundary for the plane, which is used to set up all instance
    private const float C_MIN_BOUNDARY_X = -3.0f;
    private const float C_MAX_BOUNDARY_X = 7.0f;
    private const float C_MIN_BOUNDARY_Z = -4.0f;
    private const float C_MAX_BOUNDARY_Z = 4.5f;

    void Start()
    {
#if UNITY_STANDALONE_WIN
        StartCoroutine(OnMouseDown());
#endif
#if UNITY_ANDROID
        //将物体由世界坐标系转换为屏幕坐标系
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z));
#endif
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    //得到现在鼠标的2维坐标系位置
                    Vector3 curScreenSpace = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, screenSpace.z);
                    //将当前鼠标的2维位置转换成3维位置，再加上鼠标的移动量
                    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
                    //curPosition就是物体应该的移动向量赋给transform的position属性
                    transform.position = curPosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (!IsPositioned(gameObject))
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator OnMouseDown()
    {
        //将物体由世界坐标系转化为屏幕坐标系 ，由vector3 结构体变量ScreenSpace存储，以用来明确屏幕坐标系Z轴的位置  
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        //完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系，2只有三维的情况下才能来计算鼠标位置与物体的距离，offset即是距离  
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        Debug.Log("down");

        //当鼠标左键按下时  
        while (Input.GetMouseButton(0))
        {
            //得到现在鼠标的2维坐标系位置  
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
            //将当前鼠标的2维位置转化成三维的位置，再加上鼠标的移动量  
            Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            //CurPosition就是物体应该的移动向量赋给transform的position属性        
            transform.position = CurPosition;
            //这个很主要  
            yield return new WaitForFixedUpdate();
        }


    }

    bool IsPositioned(GameObject gameObject)
    {
        return gameObject.transform.position.x >= C_MIN_BOUNDARY_X &&
            gameObject.transform.position.x <= C_MAX_BOUNDARY_X &&
            gameObject.transform.position.z >= C_MIN_BOUNDARY_Z &&
            gameObject.transform.position.z <= C_MAX_BOUNDARY_Z;
    }

}
