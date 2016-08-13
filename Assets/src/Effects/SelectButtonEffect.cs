using UnityEngine;
using System.Collections;

public class SelectButtonEffect : MonoBehaviour {

	public SpriteRenderer spriteRenderer;

	public string buttonType = "";
	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.spriteRenderer.color = new Color(1, 1, 1, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (buttonType == "MediatorのType")
		{
			this.spriteRenderer.color = new Color(1, 1, 1, 1.0f);
		} else {
			this.spriteRenderer.color = new Color(1, 1, 1, 0.2f);
		}
	}

}
