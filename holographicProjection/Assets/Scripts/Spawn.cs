using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {

	public GameObject prefab;

	public override void OnStartServer()
	{
		var spawnPosition = new Vector3(1.94916f,0.3137173f,-0.2111676f);

		var spawnRotation = Quaternion.Euler( 
			0.0f, 
			Random.Range(0,180), 
			0.0f);

		var ob = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
		NetworkServer.Spawn(ob);
	}
}