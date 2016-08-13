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

	protected Sprite DefaultSprite;

	// ダメージや成功時のエフェクト発動
	protected bool statusEffecting = false;

	protected int prevDamage = 0;
	protected int prevKillCount = 0;

	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.spriteRenderer.color = new Color(1, 1, 1, count);
		DefaultSprite = spriteRenderer.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
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
				spriteRenderer.sprite = DefaultSprite;
			}
		}

		this.spriteRenderer.color = new Color(count, count, count, count);

		// 成功時
		if (useEffectDamage && prevKillCount != GameMediator.getKillCount ())
		{
			spriteRenderer.sprite = SuccessSprite;
		}

		// ダメージの時
		if (useEffectDamage && prevDamage != GameMediator.getDamageCount ())
		{
			spriteRenderer.sprite = DamageSprite;
		}

		prevDamage = GameMediator.getDamageCount ();
		prevKillCount = GameMediator.getKillCount ();


	}

}
