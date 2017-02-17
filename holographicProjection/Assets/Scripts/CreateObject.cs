using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class CreateObject : MonoBehaviour {

	// Use this for initialization
    //add prefabs
    public GameObject[] buildingPrefabs;

	public GameObject selectedObject;

    // Boundary for the plane, which is used to set up all instance
    private const float C_MIN_BOUNDARY_X = -3.0f;
    private const float C_MAX_BOUNDARY_X = 7.0f;
    private const float C_MIN_BOUNDARY_Z = -4.0f;
    private const float C_MAX_BOUNDARY_Z = 4.5f;

    //Boundray for cancel button
    private const float C_MIN_CANCEL_X = 4.4f;
    private const float C_MAX_CANCEL_X = 6.4f;
    private const float C_MIN_CANCEL_Z = -4.3f;
    private const float C_MAX_CANCEL_Z = -3.7f;

    //current second for down and up
    private const float fClickedSpanTime = 0.2f;
    private const string C_BUTTONGROUP_NAME = "DetailButtonGroup";
    private const string C_SCENE_NAME = "scene-background";
    GameObject canvasObject = null;
    GameObject childButtongroupObj = null;
    float fDownTime = 0;
    float fUpTime = 0;
    //clicked object and position
    GameObject clickedGameObject = null;
    Vector3 clickedObjectPosition = new Vector3(0f, 0f, 0f);
    // is clicked ui
    bool bIsClickedUI = false;
    bool bIsClickedCancelButton = false;

    void Awake()
    {
        GameObject obj = GameObject.Find(C_BUTTONGROUP_NAME);
        if (obj)
        {
            //get button group id and hide it
            canvasObject = obj.transform.parent.gameObject;
            childButtongroupObj = canvasObject.transform.GetChild(0).gameObject;
            obj.SetActive(false);
        }
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
        {
            bIsClickedUI = EventSystem.current.IsPointerOverGameObject();
            // time record
            fDownTime = Time.time;
            // Get cliked object
            clickedGameObject = null;
        #if UNITY_ANDROID
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        #else
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        #endif
            RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
                clickedGameObject = hit.collider.gameObject;
                clickedObjectPosition = clickedGameObject.transform.position;
                bIsClickedCancelButton = IsInCancelButton(hit.point);
				print("I'm looking at " + hit.transform.name);// Name for hitted object
                print("hitted point: " + hit.point);
                print("is in cancel button: " + IsInCancelButton(hit.point));

                // Ctreate object instance
                string strObjectName = hit.transform.name;
                switch(strObjectName)
                {
                    case "1":
                        CreateInstance(0, hit);
                        break;
                    case "2":
                        CreateInstance(1, hit);
                        break;
                    case "3":
                        CreateInstance(2, hit);
                        break;
                    case "3.1":
                        CreateInstance(3, hit);
                        break;
                    case "4":
                        CreateInstance(4, hit);
                        break;
                    case "5":
                        CreateInstance(5, hit);
                        break;
                    case "6":
                        CreateInstance(6, hit);
                        break;
                    case "7":
                        CreateInstance(7, hit);
                        break;
                    case "10":
                        CreateInstance(8, hit);
                        break;
                    case "11":
                        CreateInstance(9, hit);
                        break;
                    case "12":
                        CreateInstance(10, hit);
                        break;
                    case "13":
                        CreateInstance(11, hit);
                        break;
                    case "14":
                        CreateInstance(12, hit);
                        break;
                    default:
                        break;
                }
			}
		}
        // hide buttongroup when clicked object is moved
        if (clickedGameObject && clickedGameObject.transform.position != clickedObjectPosition)
        {
            childButtongroupObj.SetActive(false);
        }
        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // diff time
            fUpTime = Time.time;
            float diff = fUpTime - fDownTime;
            if (diff < fClickedSpanTime)
            {
                if ((bIsClickedUI && !bIsClickedCancelButton)|| (clickedGameObject && IsPositioned(clickedGameObject) && C_SCENE_NAME != clickedGameObject.transform.name))
                {
                    //save instance
					selectedObject = clickedGameObject;
//                    int intanceId = clickedGameObject.GetInstanceID();
//                    PlayerPrefs.SetInt("selected", intanceId);
                    childButtongroupObj.SetActive(true);
                    bIsClickedUI = false;
                }
                else
                {
                    childButtongroupObj.SetActive(false);
					selectedObject = null;
                }
            }

            if (!IsPositioned(gameObject))
            {
                Destroy(gameObject);
            }
        }
	}

    //create object instance
    void CreateInstance(int index, RaycastHit hit)
    {
        GameObject instance = (GameObject)Instantiate(buildingPrefabs[index]);
        GameObject parentObject = GameObject.Find(C_SCENE_NAME);
        instance.transform.parent = parentObject.transform;
        Vector3 v3Pos = new Vector3(hit.point.x + 0.05f, hit.point.y/2+0.01f, hit.point.z + 0.05f);
        instance.transform.position = v3Pos;
        NetworkServer.Spawn(instance);
    }

    bool IsPositioned(GameObject gameObject)
    {
        return gameObject.transform.position.x >= C_MIN_BOUNDARY_X &&
            gameObject.transform.position.x <= C_MAX_BOUNDARY_X &&
            gameObject.transform.position.z >= C_MIN_BOUNDARY_Z &&
            gameObject.transform.position.z <= C_MAX_BOUNDARY_Z;
    }

    bool IsInCancelButton(Vector3 points)
    {
        return points.x >= C_MIN_CANCEL_X &&
            points.x <= C_MAX_CANCEL_X &&
            points.z >= C_MIN_CANCEL_Z &&
            points.z <= C_MAX_CANCEL_Z;
    }

    public void OnCancelButton()
    {
        childButtongroupObj.SetActive(false);
    }

}
