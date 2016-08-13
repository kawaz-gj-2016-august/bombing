using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textFlashing : MonoBehaviour {

	public GameObject lblPressKeyMessage;
	public AudioSource sndSrc;
	public AudioClip sndDecide;

	private bool movingSceneFlag = false;
	private int frameCount = 0;
	public GameObject back;

	// Use this for initialization
	void Start() {
		//TODO: スタート画面BGMを再生
		//sndSrc.clip =
		//sndSrc.Play();
	}

	// Update is called once per frame
	void Update() {
		if(this.movingSceneFlag == true) {
			return;
		}

		//Press Any Keyを点滅
		if(++this.frameCount > 40) {
			this.frameCount = 0;
			lblPressKeyMessage.SetActive(true);
		} else if(this.frameCount % 20 == 0) {
			lblPressKeyMessage.SetActive(false);
		}

		//キー入力があればゲーム画面へ進む
		if(Input.anyKeyDown == true) {
			lblPressKeyMessage.SetActive(false);
			this.movingSceneFlag = true;
			StartCoroutine(this.moveScene());
		}

		if (movingSceneFlag == true)
		{
			back.GetComponent<Image>().CrossFadeColor(Color.black, sndSrc.clip.length, false, false);
		}

	}

	/// <summary>
	/// シーン遷移
	/// </summary>
	private IEnumerator moveScene() {
		//決定音再生
		sndSrc.clip = sndDecide;
		sndSrc.Play();
		yield return new WaitForSeconds(sndSrc.clip.length);

		//シーン遷移
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main Scene", UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
