using UnityEngine;
using System.Collections;

public class MainSceneUI : MonoBehaviour {

	public const int MaxGunPower = 1000;
	public int GunPower = 0;
	public int Score = 0;
	private int tempPower = 0;
	private int tempScore = 0;

	/// <summary>
	/// 残弾ゲージを滑らかに増減します。
	/// </summary>
	public void IncrementGunPower(int value) {
		this.tempPower += value;
	}

	/// <summary>
	/// スコアを滑らかに増減します。
	/// </summary>
	public void IncrementScore(int value) {
		this.tempScore += value;
	}

	// Use this for initialization
	void Start() {

	}

	[SerializeField]
	public UnityEngine.UI.RawImage meterObject;

	[SerializeField]
	public UnityEngine.UI.Text scoreLabel;

	[SerializeField]
	public UnityEngine.UI.Text gpLabel;

	/// <summary>
	/// 滑らかな増減処理を行います
	/// </summary>
	private void applyIncrement(ref int temp, ref int dest, int min, int max) {
		int value = (System.Math.Abs(temp) / 10 > 0) ? System.Math.Abs(temp) / 10 : 1;
		dest += value * (temp > 0 ? 1 : -1);
		temp += value * (temp > 0 ? -1 : 1);
		dest = System.Math.Min(max, System.Math.Max(dest, min));
	}

	// Update is called once per frame
	void Update() {
		//残弾ゲージを滑らかに増減
		if(this.tempPower != 0) {
			this.applyIncrement(ref this.tempPower, ref this.GunPower, 0, MaxGunPower);
		}

		//スコアを滑らかに増減
		if(this.tempScore != 0) {
			this.applyIncrement(ref this.tempScore, ref this.Score, 0, int.MaxValue);
		}

		//UI類を更新
		meterObject.uvRect = new Rect(0, 0, this.GunPower / (float)MaxGunPower, 1.0f);
		meterObject.transform.localScale = new Vector3(this.GunPower / (float)MaxGunPower, 1.0f, 1.0f);
		scoreLabel.text = "Score: " + string.Format("{0,10}", this.Score);
		gpLabel.text = "GP: " + string.Format("{0,4}", this.GunPower);
	}

	public void TestGauge() {
		this.IncrementGunPower(100);
		this.IncrementScore(100000);
	}
}
