using UnityEngine;
using System.Collections;

public class SpawnSquare : MonoBehaviour {

	const int MAX_SPAWNED = 500;
	public GameObject spriteToSpawn;
	static private int numberSpawned;

	// Use this for initialization
	void Start () {
		Debug.Log("Spawner loaded.");
		spriteToSpawn.transform.position = new Vector3(0, 4.5f, 1);
		spriteToSpawn.transform.localScale = new Vector3(1, 1, 1);
	}

	// Update is called once per frame
	void Update () {
		if (numberSpawned < MAX_SPAWNED) {
			GameMediator.addEnemy(Instantiate(spriteToSpawn));
			numberSpawned++;
		}
	}

	static public void spriteDestroyed () {
		numberSpawned--;
	}

}
