using UnityEngine;
using System.Collections;

public class BackgroundEffect : MonoBehaviour {

	public Sprite DamageSprite;
	public Sprite SuccessSprite;
	public bool useEffectDamage = false;

	public SpriteRenderer spriteRenderer;
	// Use this for initialization
	public float startCount = 0.3f;
	protected float limitCount = 1.0f;
	protected float count = 0.3f;
	protected bool reverse = false;
	protected float addCount = 0.01f;

	// ダメージや成功時のエフェクト発動
	protected bool statusEffecting = false;


	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.spriteRenderer.color = new Color(1, 1, 1, count);
	}
	
	// Update is called once per frame
	void Update () {
		// ダメージ検知したら
		if (statusEffecting)
		{
			statusEffecting = true;
			count = startCount;
			reverse = false;
		}
		if (!reverse)
		{
			count += addCount;
			if (limitCount <= count)
			{
				reverse = true;
			}
		} else {
			count -= addCount;
			if (startCount >= count)
			{
				count = startCount;
				reverse = false;
				statusEffecting = false;
			}
		}

		this.spriteRenderer.color = new Color(count, count, count, count);

		// ダメージの時
		if (useEffectDamage && statusEffecting)
		{
			spriteRenderer.sprite = DamageSprite;
		}


	}

}
