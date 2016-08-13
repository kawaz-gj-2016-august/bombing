using UnityEngine;
using System.Collections;
using System;

public class MainSceneUI : MonoBehaviour {

	public UnityEngine.UI.RawImage meterObject;
	public UnityEngine.UI.Text scoreLabel;
	public UnityEngine.UI.Text gpLabel;
	public UnityEngine.UI.Text lblbombNormal;
	public UnityEngine.UI.Text lblbombLure;
	public UnityEngine.UI.Text lblbombPack;
	public UnityEngine.UI.Image imgbombNormal;
	public UnityEngine.UI.Image imgbombLure;
	public UnityEngine.UI.Image imgbombPack;
	public GameMediator gameMediator;

	public const int MaxGunPower = 1000;
	static public bool isGameOver = false;
	static private int GunPower = 0;
	static private int Score = 0;
	static private int tempPower = 0;
	static private int tempScore = 0;

	/// <summary>
	/// 残弾ゲージを滑らかに増減します。
	/// </summary>
	static public void setGunPowder(int value) {
		if(value < 0) {
			isGameOver = true;
		}
		tempPower = Math.Min(MaxGunPower, Math.Max(value, 0));
	}

	/// <summary>
	/// スコアを滑らかに増減します。
	/// </summary>
	static public void setScore(int value) {
		tempScore = Math.Min(int.MaxValue, Math.Max(value, 0));
	}

	/// <summary>
	/// 滑らかな増減処理を行います
	/// </summary>
	/*private void applyIncrement(ref int temp, ref int dest){//, int min, int max) {
		if(temp == dest) {
			return;
		}
		int value = (Math.Abs(temp) / 10 > 0) ? Math.Abs(temp) / 10 : 1;
		dest += value * (temp > 0 ? 1 : -1);
		temp += value * (temp > 0 ? -1 : 1);
		//dest = Math.Min(max, Math.Max(dest, min));
	}*/
	private void applyIncrement(ref int temp, ref int dest) {
		if(temp == dest) {
			return;
		}

		int delta = Math.Abs(temp - dest);
		int d = ((delta / 10 > 0) ? delta / 10 : (temp != dest ? 1 : 0));

		dest += d * (temp - dest > 0 ? 1 : -1);
	}

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		if(isGameOver == true) {
			//TODO: ゲームオーバー(RESULT)移行
		}

		//数値変動
		applyIncrement(ref tempPower, ref GunPower);
		applyIncrement(ref tempScore, ref Score);

		//メーター更新
		meterObject.uvRect = new Rect(0, 0, 1.0f, GunPower / (float)MaxGunPower);
		meterObject.transform.localScale = new Vector3(1.0f, GunPower / (float)MaxGunPower, 1.0f);
		
		//数値表示更新
		scoreLabel.text = "Score: " + string.Format("{0:D9}", Score);
		gpLabel.text = "GP: " + string.Format("{0:D4}", GunPower) + (GunPower >= MaxGunPower && gameMediator.gunpowder > MaxGunPower ? "+" : "");
		lblbombNormal.text = string.Format("- {0:D3}", gameMediator.cost[0]);
		lblbombLure.text = string.Format("- {0:D3}", gameMediator.cost[1]);
		lblbombPack.text = string.Format("- {0:D3}", gameMediator.cost[2]);

		//ボム種別のアクティブ表示切替
		imgbombNormal.color = new Color(1.0f, 1.0f, 1.0f, (gameMediator.bombType == 0) ? 1.0f : 0.5f);
		imgbombLure.color = new Color(1.0f, 1.0f, 1.0f, (gameMediator.bombType == 1) ? 1.0f : 0.5f);
		imgbombPack.color = new Color(1.0f, 1.0f, 1.0f, (gameMediator.bombType == 2) ? 1.0f : 0.5f);
	}
}
