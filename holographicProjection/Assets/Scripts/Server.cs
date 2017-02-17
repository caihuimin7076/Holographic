using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

	NetworkManager networkManager;

	void Awake(){
		networkManager = GetComponent<NetworkManager> ();
	}

	// Use this for initialization
	void Start () {
		networkManager.StartHost ();
	}
}
