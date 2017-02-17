using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Client : MonoBehaviour {

	NetworkManager networkManager;

	void Awake(){
		networkManager = GetComponent<NetworkManager> ();
	}

	// Use this for initialization
	void Start () {
		networkManager.networkAddress = "192.168.0.63";
		networkManager.StartClient ();
	}
}
