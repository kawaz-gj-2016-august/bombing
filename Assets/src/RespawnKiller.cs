using UnityEngine;
using System.Collections;


/**
 * Class RespawnKiller
 * このスクリプトがついたGameObjectはリスキルされる
 */
public class RespawnKiller : MonoBehaviour {

	public int framesUntilKill;
	private int frames = 0;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		frames++;
		if (frames > framesUntilKill)
		{
			Destroy(gameObject);
		}
	}
}
