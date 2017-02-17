using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Utility;
using UnityEngine;
//using UnityEditor;

public class detailView : MonoBehaviour {
	Text rotationText;
	bool enableDetail = false;//true represent the object is in detailView,false represent the object is ont in detailview
	Vector3 oldPosition;
	Vector3 sceneBgPosition;
	void Start () {
		gameObject.GetComponent<AutoMoveAndRotate> ().enabled = false;
		sceneBgPosition = GameObject.Find ("scene-background").transform.position;
		oldPosition = gameObject.transform.position;
		print("I'm looking at " + gameObject);//name for hitted object
		print ("oldPosition:"+oldPosition);
	}
	void Update(){
	
	}
	public void Click(){
		
		if (enableDetail) {
			enableDetail = false;
			gameObject.GetComponent<AutoMoveAndRotate> ().enabled = false;
			gameObject.transform.position=oldPosition;
			gameObject.transform.localScale=new Vector3(1,1,1);
			gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
			//gameObject.transform.Rotate (new Vector3(0,0,0));
		} else {
			enableDetail = true;
			gameObject.GetComponent<AutoMoveAndRotate> ().enabled = true;
			gameObject.transform.position=sceneBgPosition;
			gameObject.transform.localScale=new Vector3(System.Convert.ToSingle(1.5),System.Convert.ToSingle(1.5),System.Convert.ToSingle(1.5));
		}
	}
}
