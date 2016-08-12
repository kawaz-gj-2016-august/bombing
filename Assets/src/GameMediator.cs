﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMediator : MonoBehaviour {

	public GameObject targetX;
	public GameObject bombEffect;
	public float gunpowder;
	static private List<GameObject> enemies = new List<GameObject>();
	static private List<GameObject> weapons = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		// クリック時の処理
		/**
		 * 1. クリックした場所に爆弾を落とす。
		 *
		 */
		if (Input.GetMouseButtonDown(0))
		{
			dropBomb(0);
		}

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
		// TODO: 爆風の出現を遅らせる+自陣から爆弾が飛ぶアニメも入れる
		Instantiate(bombEffect, target, Quaternion.identity); // 爆風 - これをもとにダメージ計算
	}

	/**
	 * bombDamage - 爆風にかかったかどうかをチェック
	 * @param Vector3 position 爆弾の位置
	 * @param float   dRange   ダメージが通る半径
	 */
	static void bombDamage(Vector3 position, float dRange)
	{
		// 敵について
		foreach (GameObject enemy in enemies)
		{
			// positionとobjectPositionの距離をとってdRange内にあればヒット
			if (Vector3.Distance(enemy.transform.position, position) < dRange)
			{
				// TODO: ここに敵をぶっとばす処理を入れる
			}
		}
		// 武器について
		foreach (GameObject weapon in weapons)
		{
			objectPosition = weapon.transform.position;
			if (Vector3.Distance(weapon.transform.position, position) < dRange)
			{
				// TODO: 誘爆処理、ルアー破壊処理など
			}
		}
	}
}
