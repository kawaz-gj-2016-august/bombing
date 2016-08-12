using UnityEngine;
using System.Collections;

public class GunPowerMeter : MonoBehaviour {

	public const int MaxGunPower = 1000;
	public int GunPower = 0;

	/// <summary>
	/// 残弾ゲージを増減します。
	/// </summary>
	public void IncrementGunPower(int value) {
		if(value < 0) {
			this.GunPower = 0;
		} else if(MaxGunPower < value) {
			this.GunPower = MaxGunPower;
		} else {
			this.GunPower = value;
		}
	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		var meterObject = GetComponent<UnityEngine.UI.RawImage>();
		if(meterObject != null) {
			meterObject.uvRect = new Rect(0, 0, this.GunPower / (float)MaxGunPower, 1.0f);
			meterObject.transform.localScale = new Vector3(this.GunPower / (float)MaxGunPower, 1, 1);
		}
	}
}
