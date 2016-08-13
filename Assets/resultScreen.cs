using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class resultScreen : MonoBehaviour {

	public GameObject scoreLabel;

	// Use this for initialization
	void Start () {
		scoreLabel.GetComponent<Text>().text = String.Format("score    {0:D9}", Communicator.getScore());
	}

	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown == true) {
			GameMediator.reset();
			UnityEngine.SceneManagement.SceneManager.LoadScene("Main Scene", UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}
}
