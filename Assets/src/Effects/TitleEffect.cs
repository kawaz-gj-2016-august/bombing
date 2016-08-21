using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleEffect : MonoBehaviour
{
	public Image spriteRenderer;
	public float startCount = 0.3f;

	protected float limitCount = 1.0f;
	protected float count = 0.3f;
	protected bool reverse = false;
	protected float addCount = 0.005f;

	void Start()
	{
		this.spriteRenderer.color = new Color(1, 1, 1, count);
	}

	// Update is called once per frame
	void Update()
	{

		// 外的要因でスタートカウントを下げるまで続ける
		if (startCount <= this.spriteRenderer.color.a)
		{

			if (!reverse)
			{
				count += addCount;
				if (limitCount <= count)
				{
					reverse = true;
				}
			}
			else {
				count -= addCount;
				if (startCount >= count)
				{
					count = startCount;
					reverse = false;
				}
			}

			this.spriteRenderer.color = new Color(count, count, count, count);
		}


	}

}
