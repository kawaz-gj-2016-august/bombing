using UnityEngine;
using System.Collections;

public enum _bombType
{
	NORMAL,
	PACK,
	COUNT
}

public class BombEffector : MonoBehaviour {

	public _bombType bombType;
	public GameObject bombEffect;
	public float damageRange;
	private bool triggered;

	// Use this for initialization
	void Start () {
		switch (bombType)
		{
			case _bombType.NORMAL:
			{
				// 普通の爆弾
				triggered = true; // これはいきなり着火する
				break;
			}
			case _bombType.PACK:
			{
				// 置き爆弾
				triggered = false; // いきなり着火しない
				break;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (triggered)
		{
			// bombEffectは1.0fになるように調節している。damageRangeによってその大きさを変更
			bombEffect.transform.localScale = new Vector3(damageRange, damageRange, 1.0f);
			Instantiate(bombEffect, this.transform.position, Quaternion.identity);
			GameMediator.bombDamage(this.transform.position, damageRange);
			Destroy(this.gameObject);
		}
	}


	/**
	 * triggeredをtrueにする。種類によってはいきなりtrueになっているやつもあるけど
	 */
	public void ignite ()
	{
		triggered = true;
	}
}
