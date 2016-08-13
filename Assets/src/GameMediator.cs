using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameMediator : MonoBehaviour {

	public GameObject targetX;
	public GameObject bomb;
	public GameObject lure;
	public GameObject powderPack;
	public GameObject cannon;
	public GameObject bombArc;
	public int gunpowder;
	public int score;
	public int[] cost = new int[3];
	static private List<GameObject> enemies = new List<GameObject>();
	static private List<GameObject> powderPacks = new List<GameObject>();
	static private List<GameObject> lures = new List<GameObject>();
	public int bombType = 0;
	public string[] bombKeyType = new string[] {"1", "2", "3"};

	static protected int killCount = 0;
	static protected int damageCount = 0;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		// 爆弾の種類の切り替えを行う
		for (int i = 0; i < bombKeyType.Length; i++)
		{
			if (Input.GetKey(bombKeyType[i]))
			{
				bombType = i;
			}
		}

		// クリック時の処理
		if (Input.GetMouseButtonDown(0))
		{
			if (gunpowder >= cost[bombType])
			{
				dropBomb(bombType);
				gunpowder -= cost[bombType];
			}
			else
			{
				// 足らない
			}
		}

		if (gunpowder < 0)
		{
			// GAME!
		}

		gunpowder += 1;

		MainSceneUI.setGunPowder(gunpowder);
		MainSceneUI.setScore(score);
	}

	/**
	 * dropbomb - 爆弾を落とす
	 * @param int bType おいた爆弾のタイプ
	 */
	void dropBomb(int bType)
	{
		Vector3 target;
		// 2Dの時も3Dのやり方を踏襲してOK。最後にZを切り捨てる
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target.z = transform.position.z; // Z切り捨て
		Instantiate(targetX, target, Quaternion.identity); // ターゲットのXマーク
		switch (bombType) {
			case 0:
			{
				// TODO: 爆風の出現を遅らせる+自陣から爆弾が飛ぶアニメも入れる
				float angle = Vector3.Angle(Vector3.down, cannon.transform.position - target);
				angle = angle * (target.x < 0 ? 1 : -1);
				GameObject arc = (GameObject)Instantiate(bombArc, cannon.transform.position, Quaternion.identity);
				arc.transform.Rotate(0, 0, angle, Space.Self);
				arc.transform.localScale = new Vector3(1, Vector3.Distance(target, cannon.transform.position), 1);
				Debug.Log(Vector3.Distance(target, cannon.transform.position));
				Instantiate(bomb, target, Quaternion.identity);
				break;
			}
			case 1:
			{
				// TODO: ルアー
				GameObject genLure = (GameObject)GameObject.Instantiate(lure, target, Quaternion.identity);
				addLure(genLure);
				break;
			}
			case 2:
			{
				// TODO: 置き爆弾
				GameObject pack = (GameObject)GameObject.Instantiate(powderPack, target, Quaternion.identity);
				addPowderPack(pack);
				break;
			}
		}
	}

	/**
	 * bombDamage - 爆風にかかったかどうかをチェック
	 * @param Vector3 position 爆弾の位置
	 * @param float   dRange   ダメージが通る半径
	 */
	static public void bombDamage(Vector3 position, float dRange)
	{
		// 敵について

		List<GameObject> enemyToDelete = new List<GameObject>();
		foreach (GameObject enemy in enemies)
		{
			// positionとobjectPositionの距離をとってdRange内にあればヒット
			if (Vector3.Distance(enemy.transform.position, position) < dRange)
			{
				// TODO: ここに敵をぶっとばす処理を入れる
				Destroy(enemy.gameObject);
				enemyToDelete.Add(enemy);
				SpawnSquare.spriteDestroyed();

				killCount += 1;
			}
		}
		enemies.RemoveAll(l => enemyToDelete.Contains(l));
		// 置き爆弾について
		List<GameObject> packToDelete = new List<GameObject>();
		foreach (GameObject powderPack in powderPacks)
		{
			if (Vector3.Distance(powderPack.transform.position, position) < dRange)
			{
				// まずは対象となった置き爆弾のBombEffectorスクリプトを取得
				BombEffector effectorSrc = (BombEffector) powderPack.GetComponent(typeof(BombEffector));
				effectorSrc.ignite(); // そして点火
				packToDelete.Add(powderPack); // 削除準備
			}
		}
		// 置き爆弾の削除
		powderPacks.RemoveAll(p => packToDelete.Contains(p));
		List<GameObject> lureToDelete = new List<GameObject>();
		// ルアーについて
		foreach (GameObject lure in lures)
		{
			if (Vector3.Distance(lure.transform.position, position) < dRange)
			{
				Destroy(lure.gameObject);
				lureToDelete.Add(lure);
			}
		}
		lures.RemoveAll(l => lureToDelete.Contains(l));
	}

	/**
	 * 敵を追加する
	 */
	static public void addEnemy(GameObject enemy)
	{
		enemies.Add(enemy);
	}

	/**
	 * 敵を取り除く
	 */
	static public void removeEnemy(GameObject enemy)
	{
		// 「enemiesの要素の中でenemyに一致したもの全てを取り除く」
		enemies.RemoveAll(e => e == enemy);
	}

	/**
	 * ルアーを追加する
	 */
	static public void addLure(GameObject lure)
	{
		lures.Add(lure);
	}

	/**
	 * ルアーのリストを得る
	 */
	static public List<GameObject> getLures()
	{
		return lures;
	}

	/**
	 * 置き爆弾を追加する
	 */
	static public void addPowderPack(GameObject powderPack)
	{
		powderPacks.Add(powderPack);
	}

	static public List<GameObject> getEnemies()
	{
		return enemies;
	}


	public int getGunPowder()
	{
		return gunpowder;
	}

	static public int getKillCount()
	{
		return killCount;
	}

	static public int getDamageCount()
	{
		return damageCount;
	}

	static public void addDamageCount(int damage = 1)
	{
		damageCount += damage;
	}
}
