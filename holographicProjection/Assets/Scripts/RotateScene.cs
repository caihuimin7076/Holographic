using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class RotateScene : MonoBehaviour {

	bool enableRotate = false;
	Text rotationText;
	Image image;
	Color color;
	// Use this for initialization
	void Start () {
		rotationText = GameObject.Find ("rotation").GetComponent<Text> ();
		image = GameObject.Find ("RightTopButton").GetComponent<Image> ();

	}

	// Update is called once per frame
	void Update () {

	}

	public void Click(){
		if (enableRotate) {
			enableRotate = false;
			transform.localRotation = Quaternion.Euler(0,0,0);
			rotationText.text="Rotate";
			color.r = 0.16f;
			color.g = 0.61f;
			color.b = 0.61f;
			color.a = 0.5f;
			image.color=color;
		} else {

			enableRotate = true;
			rotationText.text="Reset";

			color.r = 0.61f;
			color.g = 0.16f;
			color.b = 0.16f;
			color.a = 0.5f;
			image.color=color;

		}

		gameObject.GetComponent<AutoMoveAndRotate> ().enabled = enableRotate;
	}
}
//	void OnGUI(){
//		GUI.backgroundColor = Color.gray;
//		if(GUI.Button(new Rect(10,10,80,30),"rotateLeft")){
//			transform.Rotate(0,-25*Time.deltaTime,0,Space.Self);
//}
//	}
