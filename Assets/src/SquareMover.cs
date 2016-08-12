using UnityEngine;
using System.Collections;

public class SquareMover : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		pos.y += 0.05f;
		//pos.x = Random.Range(-10.0f, 10.0f);
		this.transform.position = pos;
		if (pos.y > 3) {
			Destroy(gameObject);
			SpawnSquare.spriteDestroyed();
		}
	}
}
