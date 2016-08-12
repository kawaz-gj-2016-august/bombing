using UnityEngine;
using System.Collections;

public class textFlashing : MonoBehaviour {

	private int frameCount = 0;

	[SerializeField]
	public GameObject obj;

	// Use this for initialization
	void Start() {
		this.frameCount = 0;
	}

	// Update is called once per frame
	void Update() {
		if(++this.frameCount > 60) {
			this.frameCount = 0;
			obj.SetActive(true);
		} else if(this.frameCount % 30 == 0) {
			obj.SetActive(false);
		}
		Debug.Log(frameCount);
	}
}
