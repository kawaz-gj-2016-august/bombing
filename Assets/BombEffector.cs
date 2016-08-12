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
	public float dRange;
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
			GameMediator.bombDamage(this.transform.position, dRange);
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
