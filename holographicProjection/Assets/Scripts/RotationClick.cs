using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationClick : MonoBehaviour {
	public float rotation;
	GameObject myobject;

	CreateObject createObjectScript;
	// Use this for initialization
	void Start () {
		createObjectScript = Camera.main.GetComponent<CreateObject> ();
	}

	// Update is called once per frame
	void Update () {

	}
	public void MyClick()
	{

		myobject = createObjectScript.selectedObject;
		myobject.transform.rotation *= Quaternion.Euler(0, rotation, 0);
	}
}
