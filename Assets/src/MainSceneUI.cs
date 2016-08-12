using UnityEngine;
using System.Collections;
using System;

public class MainSceneUI : MonoBehaviour {

	public UnityEngine.UI.RawImage meterObject;
	public UnityEngine.UI.Text scoreLabel;
	public UnityEngine.UI.Text gpLabel;

	public const int MaxGunPower = 1000;
	static public int GunPower = 0;
	static public int Score = 0;
	static private int tempPower = 0;
	static private int tempScore = 0;

	/// <summary>
	/// 残弾ゲージを滑らかに増減します。
	/// </summary>
	public void IncrementGunPowder(int value) {
		tempPower += value;
	}

	/// <summary>
	/// スコアを滑らかに増減します。
	/// </summary>
	public void IncrementScore(int value) {
		tempScore += value;
	}

	/// <summary>
	/// 残弾ゲージを滑らかに増減します。
	/// </summary>
	static public void setGunPowder(int value) {
		tempPower = value;
	}

	/// <summary>
	/// スコアを滑らかに増減します。
	/// </summary>
	static public void setScore(int value) {
		tempScore = value;
	}

	// Use this for initialization
	void Start() {

	}

	/// <summary>
	/// 滑らかな増減処理を行います
	/// </summary>
	/*private void applyIncrement(ref int temp, ref int dest, int min, int max) {
		int value = (System.Math.Abs(temp) / 10 > 0) ? System.Math.Abs(temp) / 10 : 1;
		dest += value * (temp > 0 ? 1 : -1);
		temp += value * (temp > 0 ? -1 : 1);
		dest = System.Math.Min(max, System.Math.Max(dest, min));
	}*/
	private void applyIncrement(ref int temp, ref int dest) {
		int delta = Math.Abs(temp - dest);
		int d = ((delta / 10 > 0) ? delta / 10 : (temp != dest ? 1 : 0));
		dest = dest + d * (temp - dest > 0 ? 1 : -1);
	}

	// Update is called once per frame
	void Update() {
		applyIncrement(ref tempPower, ref GunPower);

		//スコアを滑らかに増減
		applyIncrement(ref tempScore, ref Score);

		//UI類を更新
		meterObject.uvRect = new Rect(0, 0, GunPower / (float)MaxGunPower, 1.0f);
		meterObject.transform.localScale = new Vector3(GunPower / (float)MaxGunPower, 1.0f, 1.0f);
		scoreLabel.text = "Score: " + string.Format("{0:D9}", Score);
		gpLabel.text = "GP: " + string.Format("{0:0000}", GunPower);
	}

	public void TestGauge() {
		IncrementGunPowder(100);
		IncrementScore(100000);
	}
}
