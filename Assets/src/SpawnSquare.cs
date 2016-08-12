using UnityEngine;
using System.Collections;

public class SpawnSquare : MonoBehaviour {

	public GameObject spriteToSpawn;
	static private int numberSpawned;

	// Use this for initialization
	void Start () {
		Debug.Log("Spawner loaded.");
	}

	// Update is called once per frame
	void Update () {
		if (numberSpawned < 15) {
			Instantiate(spriteToSpawn);
			numberSpawned++;
		}
	}

	static public void spriteDestroyed () {
		numberSpawned--;
	}

}
