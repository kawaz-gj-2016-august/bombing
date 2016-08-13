using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour {

	public AudioSource source;
	public AudioClip clip;

	// Use this for initialization
	void Start () {
		source.clip = clip;
		source.Play();
	}

	// Update is called once per frame
	void Update () {

	}
}
