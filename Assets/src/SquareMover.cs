using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareMover : MonoBehaviour {

	public AudioClip hurt;

	protected float directionX = 0f;
	protected float directionY = 0f;
	public float damageRange = 2.0f;
	protected Vector3 lureStart;
	protected float lureAngle = 0f;
	protected float lureStartRange = 0.0f;

	protected int lureFrame = 0;
	protected int lureLimitFrames = 180;
	protected bool lureFrameReverse = false;

	protected List<GameObject> usedLures;

	protected float moveSpeedRate = 2.0f;

	// Use this for initialization
	void Start () {

		// 乱数で方角を確保
		// X方向は横にいきすぎないように補正をセット
		directionY = Random.Range(0.05f, 0.18f) * 0.1f;
		directionX = System.Math.Min(Random.Range(-0.09f, 0.09f) * 0.1f, directionY) - Random.Range(0.000f, 0.005f);

		if (0 > directionX) {
			directionX = (System.Math.Max(directionX * -0.1f, 0.05f) * -0.1f) + Random.Range(0.000f, 0.005f);
		}

		// ルアーに入る閾値
		lureStartRange = Random.Range (3.0f, 5.0f);

		// 使用済みルアーリスト
		usedLures = new List<GameObject> ();


	}

	// Update is called once per frame
	void Update()
	{
		// ルアーモード
		bool inLures = false;
		Vector3 pos = gameObject.transform.position;
		List<GameObject> lures = GameMediator.getLures();


		foreach (GameObject lure in lures)
		{
			// ルアーの座標を確保
			Vector3 lurePos = lure.transform.position;
			if (!usedLures.Contains(lure) &&					// 使ったことのあるルアーリストにはいない
				Vector3.Distance(lurePos, pos) < damageRange	// ルアー
			)
			{
				// ルアーに入った
				inLures = true;

				// ルアー時間を加算
				lureFrame += 1;

				// ルアー時間が経過いたら、今のルアーから脱出する
				if (lureFrame >= lureLimitFrames) {
					lureFrame = 0;
					//lureFrameReverse = true;
					usedLures.Add (lure);
				}

				//if (lureStart == null) {
				//	lureStart = pos;
				//}

				Vector3 direction = lurePos - pos;
				if (Vector3.Distance (lurePos, pos) < lureStartRange) {
					gameObject.transform.position += direction * 0.01f;
				}

				// ルアーに入ったので次のルアーにはいかない
				break;

				/*
				// ここにきたら円運動
				gameObject.transform.position = lurePos + new Vector3(
					Mathf.Cos(lureAngle + lureStart.x),
					Mathf.Sin(lureAngle + lureStart.y) ,
					0);
				if (lureStart == pos) {
					inLures = false;
				}
				lureAngle += 0.01f;
				}*/

			}


		}

		// ルアーに入っていなければ通常移動
		if (!inLures)
		{
			pos.y -= directionY * this.moveSpeedRate;
			pos.x += directionX * this.moveSpeedRate;
			gameObject.transform.position = pos;
		}

		// 画面したまでいったら破壊
		if (pos.y < -5)
		{
			GameMediator.removeEnemy(gameObject);
			Destroy(gameObject);
			SpawnSquare.spriteDestroyed();
			GameMediator.addDamageCount(1 + (GameMediator.getKillCount() / 1000));

			GameMediator.instance.playSE(hurt);
		}
	}
}
